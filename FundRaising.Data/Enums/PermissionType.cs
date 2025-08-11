using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

namespace FundRaising.Data.Enums
{
    public enum PermissionType
    {
        [Display(Name = "User administration - add, edit, deactivate users")]
        UserAdministration = 1,
        
    }
}
