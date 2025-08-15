using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Data.Models
{
    public class GroupMembers
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public string MemberId { get; set; }
        public ApplicationUser Member { get; set; }
    }
}
