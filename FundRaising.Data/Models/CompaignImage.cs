using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FundRaising.Data.Models
{
    public class CompaignImage
    {
        public int Id { get; set; }
        public int CompaignId { get; set; }
        public string Image { get; set; }
        [JsonIgnore]
        public Compaign Compaign { get; set; }
    }
}
