using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Data.Models
{
    public class Compaign
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? RequiredAmount { get; set; }
        public decimal? RaisedAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Status { get; set; }
        public bool IsCompaignDeleted { get; set; }
        public bool IsCompaignLaunch { get; set; }
        public bool IsApproved { get; set; }
        public string? AssignedToId { get; set; }
        public ApplicationUser? AssignedTo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedById { get; set; }
        public ApplicationUser? UpdatedBy { get; set; }

        public ICollection<CompaignImage> Images { get; set; }
        public ICollection<CompaignOrganizer> Organizers { get; set; }
        public ICollection<CompaignGroup> Groups { get; set; }
        public ICollection<CompaignProduct> Products { get; set; }
    }
}
