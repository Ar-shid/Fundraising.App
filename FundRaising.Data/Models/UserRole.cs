using Microsoft.AspNetCore.Identity;

namespace FundRaising.Data.Models
{
    public class UserRole : IdentityUserRole<string>
    {
        public Role Role { get; set; }

        public ApplicationUser User { get; set; }
    }
}
