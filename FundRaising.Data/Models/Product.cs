using FundRaising.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(500)]
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
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedById { get; set; }
        public ApplicationUser? UpdatedBy { get; set; }


        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<ProductCategory> Categories { get; set; } = new List<ProductCategory>();

    }
}
