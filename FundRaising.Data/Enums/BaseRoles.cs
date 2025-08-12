
using System.ComponentModel.DataAnnotations;

namespace FundRaising.Data.Enums
{
    public enum BaseRoles
    {
        [Display(Name = "Admin")]
        Admin = 1,

        [Display(Name = "Sales Person")]
        SalesPerson = 2,

        [Display(Name = "Organizer")]
        Organizer = 3,

        [Display(Name = "Participant")]
        Participant = 3,
        
    }
}
