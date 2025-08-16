using FundRaising.Data.Models;
using FundRaising.DTO.ProductModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Services.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllAsync();
        Task<ProductViewModel> GetByIdAsync(int id);
        Task<Product> CreateAsync(ProductViewModel model, IFormFileCollection? files);
        Task<bool> UpdateAsync(ProductViewModel product, IFormFileCollection? newFiles);
        Task<bool> DeleteAsync(int id);
    }
}
