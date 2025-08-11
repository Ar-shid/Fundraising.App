using Microsoft.AspNetCore.Identity;

namespace FundRaising.Data.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Initials { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<UserRole> Roles { get; set; }
    }
}
