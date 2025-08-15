using FundRaising.Common.Mappings;
using FundRaising.Data;
using FundRaising.Data.Models;
using FundRaising.DTO.CompaignModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Services.CompaignService
{
    public class CompaignService: ICompaignService
    {
        protected readonly FundRaisingDBContext _context;
        private readonly IWebHostEnvironment _env;
        public CompaignService(FundRaisingDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IEnumerable<CompaignViewModel>> GetAllAsync()
        {
            return await _context.Compaigns
                .Include(c => c.Images)
                .Include(c => c.Organizers)
                .Include(c => c.Groups)
                .Include(c => c.AssignedTo)
                .Include(c => c.CreatedBy)
                .Include(c => c.UpdatedBy)
                .ToListAsync().To<List<CompaignViewModel>>();
        }

        public async Task<CompaignViewModel> GetByIdAsync(int id)
        {
            var result = await _context.Compaigns.Include(c => c.Images).Include(c => c.CreatedBy)
                    .FirstOrDefaultAsync(c => c.Id == id).To<CompaignViewModel>();
            return result;
        }
        public async Task<Compaign> CreateAsync(CompaignViewModel model, IFormFileCollection? files)
        {
            model.CreatedDate = DateTime.UtcNow;
            model.UpdatedDate = null;
            model.UpdatedDate = null;
            var compaign = model.To<Compaign>();
            compaign.CreatedById = model.CreatedByName;
            
            _context.Compaigns.Add(compaign);
            await _context.SaveChangesAsync();

            if (files != null)
            {
                foreach (var file in files)
                {
                    try
                    {
                        var relativePath = await SaveImageFile(file);
                        _context.CompaignImages.Add(new CompaignImage
                        {
                            CompaignId = compaign.Id,
                            Image = relativePath
                        });
                    }
                    catch (Exception ex)
                    {

                    }
                    
                }
                await _context.SaveChangesAsync();
            }

            return compaign;
        }

        public async Task<bool> UpdateAsync(CompaignViewModel compaign, IFormFileCollection? newFiles)
        {
            var existing = await _context.Compaigns
                .Include(c => c.Images)
                .FirstOrDefaultAsync(c => c.Id == compaign.Id);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(compaign);
            existing.UpdatedDate = DateTime.UtcNow;

            if (newFiles != null && newFiles.Count > 0)
            {
                foreach (var file in newFiles)
                {
                    var relativePath = await SaveImageFile(file);
                    _context.CompaignImages.Add(new CompaignImage
                    {
                        CompaignId = existing.Id,
                        Image = relativePath
                    });
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var compaign = await _context.Compaigns
                .Include(c => c.Images)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (compaign == null) return false;

            foreach (var img in compaign.Images)
            {
                DeleteImageFile(img.Image);
            }
            _context.CompaignImages.RemoveRange(compaign.Images);
            _context.Compaigns.Remove(compaign);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<string> SaveImageFile(IFormFile file)
        {
            var folderPath = Path.Combine(_env.ContentRootPath, "Images", "CompaignImages");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/Images/CompaignImages/{fileName}";
        }

        private void DeleteImageFile(string relativePath)
        {
            var fullPath = Path.Combine(_env.ContentRootPath, relativePath.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
