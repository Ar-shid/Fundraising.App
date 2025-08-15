using FundRaising.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Data.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoPath { get; set; }
        public GroupStatus Status { get; set; }
        public bool isApproved { get; set; }
        public bool isDraft { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedById { get; set; }
        public ApplicationUser? UpdatedBy { get; set; }
        public ICollection<GroupMembers> GroupMembers { get; set; }
    }
}
