using FundRaising.Common.Mappings;
using FundRaising.Data;
using FundRaising.Data.Models;
using FundRaising.DTO.ProductModels;
using FundRaising.Services.FileService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Services.ProductService
{
    public class ProductService: IProductService
    {
        protected readonly FundRaisingDBContext _context;
        private readonly IFileService _fileService;
        public ProductService(FundRaisingDBContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }
        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            return await _context.Products
                .Include(c => c.Images)
                .Include(c => c.Categories).ThenInclude(c => c.Category)
                .Include(c => c.CreatedBy)
                .Include(c => c.UpdatedBy)
                .ToListAsync().To<List<ProductViewModel>>();
        }

        public async Task<ProductViewModel> GetByIdAsync(int id)
        {
            var result = await _context.Products.Include(c => c.Images).Include(c => c.Categories).ThenInclude(c => c.Category).Include(c => c.CreatedBy)
                    .FirstOrDefaultAsync(c => c.Id == id).To<ProductViewModel>();
            return result;
        }
        public async Task<Product> CreateAsync(ProductViewModel model, IFormFileCollection? files)
        {
            model.CreatedDate = DateTime.UtcNow;
            model.UpdatedDate = null;
            model.UpdatedDate = null;
            var product = model.To<Product>();
            product.CreatedById = model.CreatedByName;
            
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            if (files != null)
            {
                foreach (var file in files)
                {
                    var relativePath = await _fileService.SaveFileAsync(file, "ProductImages");
                    _context.ProductImages.Add(new ProductImage
                    {
                        ProductId = product.Id,
                        Image = relativePath
                    });
                }
                await _context.SaveChangesAsync();
            }
            if (model.CategoryIds.Any()) {
                var productCategories = new List<ProductCategory>();
                foreach (var item in model.CategoryIds)
                {
                    var productCategory = new ProductCategory()
                    {
                        ProductId = product.Id,
                        CategoryId = item
                    };
                    productCategories.Add(productCategory);
                }
                _context.ProductCategories.AddRange(productCategories);
                await _context.SaveChangesAsync();
            }
            
            return product;
        }

        public async Task<bool> UpdateAsync(ProductViewModel product, IFormFileCollection? newFiles)
        {
            var existing = await _context.Products
                .Include(c => c.Images)
                .Include(c => c.Categories)
                .FirstOrDefaultAsync(c => c.Id == product.Id);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(product);
            existing.UpdatedDate = DateTime.UtcNow;
            existing.UpdatedById = product.UpdatedByName;
            if (newFiles != null && newFiles.Count > 0)
            {
                foreach (var file in newFiles)
                {
                    var relativePath = await _fileService.SaveFileAsync(file, "ProductImages");
                    _context.ProductImages.Add(new ProductImage
                    {
                        ProductId = existing.Id,
                        Image = relativePath
                    });
                }
            }
            _context.ProductCategories.RemoveRange(existing.Categories);
            await _context.SaveChangesAsync();
            if (product.CategoryIds.Any())
            {
                var productCategories = new List<ProductCategory>();
                foreach (var item in product.CategoryIds)
                {
                    var productCategory = new ProductCategory()
                    {
                        ProductId = product.Id,
                        CategoryId = item
                    };
                    productCategories.Add(productCategory);
                }
                _context.ProductCategories.AddRange(productCategories);
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products
                .Include(c => c.Images)
                .Include(c => c.Categories)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (product == null) return false;

            foreach (var img in product.Images)
            {
                _fileService.DeleteFile(img.Image);
            }
            _context.ProductImages.RemoveRange(product.Images);
            _context.ProductCategories.RemoveRange(product.Categories);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
