using AutoMapper;
using FundRaising.Common.Mappings;
using FundRaising.Data.Models;
using FundRaising.DTO.CompaignModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.DTO.ProductModels
{
    public class ProductCategoryViewModel: IHaveCustomMappings
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ProductCategory, ProductCategoryViewModel>()
                .ForMember(u => u.ProductName, y => y.MapFrom(u => u.Product.Name))
                .ForMember(u => u.CategoryDescription, y => y.MapFrom(u => (u.Category.Description)))
                .ForMember(u => u.CategoryName, y => y.MapFrom(u => (u.Category.Name)));
            configuration.CreateMap<ProductCategoryViewModel, ProductCategory>();

        }
    }
}
