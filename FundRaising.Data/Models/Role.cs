using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FundRaising.Data.Models
{
    public class Role : IdentityRole
    {
        public Role()
        {
            Permissions = new List<RolePermission>();
            Users = new List<UserRole>();
        }

        public ICollection<RolePermission> Permissions { get; set; }

        public ICollection<UserRole> Users { get; set; }
    }
}
