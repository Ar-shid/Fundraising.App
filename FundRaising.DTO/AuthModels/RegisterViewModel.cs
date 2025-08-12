using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FundRaising.Common.ModelConstants;

namespace FundRaising.DTO.AuthModels
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(UserConstants.FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(UserConstants.LastNameMaxLength)]
        public string LastName { get; set; }

        [MaxLength(UserConstants.MiddleNameMaxLength)]
        public string MiddleName { get; set; }
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(EmailAddressMaxLength)]
        public string  Email { get; set; }

        [MaxLength(UserConstants.PasswordMaxLength)]
        public string Password { get; set; }
        [Required]
        public string[] Roles { get; set; }
    }
}
