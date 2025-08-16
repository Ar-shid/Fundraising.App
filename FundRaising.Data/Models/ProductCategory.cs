using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FundRaising.Data.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
    }
}
