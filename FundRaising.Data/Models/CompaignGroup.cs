using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FundRaising.Data.Models
{
    public class CompaignGroup
    {
        public int Id { get; set; }
        public int CompaignId { get; set; }
        [JsonIgnore]
        public Compaign Compaign { get; set; }
        public int GroupId { get; set; }
        [JsonIgnore]
        public Group Group { get; set; }
    }
}
