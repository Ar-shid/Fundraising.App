using AutoMapper;
using FundRaising.Common.Mappings;
using FundRaising.Data.Enums;
using FundRaising.Data.Models;
using FundRaising.DTO.CompaignModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.DTO.ProductModels
{
    public class ProductViewModel: IHaveCustomMappings
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }

        public ActiveStatus Status { get; set; }

        [Precision(18, 2)]
        public decimal RegularPrice { get; set; }

        [Precision(18, 2)]
        public decimal ProfitMargin { get; set; }

        public int PurchaseQuantity { get; set; }

        public int SaleQuantity { get; set; }

        public bool IsSinglePurchase { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public string? CreatedByName { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? UpdatedByName { get; set; }

        [NotMapped]
        public IFormFileCollection? UploadImages { get; set; }
        public List<string> ImagePaths { get; set; }
        public List<int> CategoryIds { get; set; }
        public ICollection<ProductCategoryViewModel> CategoryDetails { get; set; } = new List<ProductCategoryViewModel>();
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ProductViewModel>()
                .ForMember(u => u.CreatedByName, y => y.MapFrom(u => u.CreatedBy.FirstName + " " + u.CreatedBy.LastName))
                .ForMember(u => u.UpdatedByName, y => y.MapFrom(u => u.UpdatedBy == null ? "" : (u.UpdatedBy.FirstName + " " + u.UpdatedBy.LastName)))
                .ForMember(u => u.CategoryDetails, y => y.MapFrom(u => u.Categories));
            configuration.CreateMap<ProductViewModel, Product>()
               .ForMember(u => u.Categories, y => y.Ignore());



        }
    }
}
