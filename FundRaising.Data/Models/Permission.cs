
using FundRaising.Data.Common;
using FundRaising.Data.Enums;
using System.Collections.Generic;

namespace FundRaising.Data.Models
{
    public class Permission : Entity<int>
    {
        public Permission()
        {
            Roles = new List<RolePermission>();
        }

        public PermissionType Type { get; set; }

        public string Description { get; set; }

        public int OrderBy { get; set; }

        public ICollection<RolePermission> Roles { get; set; }


    }
}
