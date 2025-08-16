using FundRaising.Common.Mappings;
using FundRaising.Data;
using FundRaising.Data.Models;
using FundRaising.DTO.GroupModels;
using FundRaising.Services.FileService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FundRaising.Services.GroupService
{
    public class GroupService: IGroupService
    {
        protected readonly FundRaisingDBContext _context;
        private readonly IFileService _fileService;
        public GroupService(FundRaisingDBContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }
        public async Task<IEnumerable<GroupViewModel>> GetAllAsync()
        {
            return await _context.Groups
                .Include(c => c.GroupMembers)
                .Include(c => c.CreatedBy)
                .ToListAsync().To<List<GroupViewModel>>();
        }

        public async Task<GroupViewModel> GetByIdAsync(int id)
        {
            var result = await _context.Groups.Include(c => c.GroupMembers).Include(c => c.CreatedBy)
                    .FirstOrDefaultAsync(c => c.Id == id).To<GroupViewModel>();
            return result;
        }
        public async Task<Group> CreateAsync(GroupViewModel model, IFormFile? file)
        {
            model.CreatedDate = DateTime.UtcNow;
            model.UpdatedDate = null;
            model.UpdatedDate = null;
            var group = model.To<Group>();
            group.CreatedById = model.CreatedByName;
            

            if (file != null)
            {
                var relativePath = await _fileService.SaveFileAsync(file, "GroupImages");
                group.LogoPath = relativePath;
            }

            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<bool> UpdateAsync(GroupViewModel group, IFormFile? newFile)
        {
            var existing = await _context.Groups
                .FirstOrDefaultAsync(c => c.Id == group.Id);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(group);
            existing.UpdatedDate = DateTime.UtcNow;
            existing.UpdatedById = group.UpdatedByName;
            if (newFile != null)
            {
                var relativePath = await _fileService.SaveFileAsync(newFile, "GroupImages");

                existing.LogoPath = relativePath;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var group = await _context.Groups
                .Include(c => c.GroupMembers)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (group == null) return false;


            _fileService.DeleteFile(group.LogoPath);
            _context.GroupMembers.RemoveRange(group.GroupMembers);
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return true;
        }

        
    }
}
