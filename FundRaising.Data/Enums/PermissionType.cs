using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

namespace FundRaising.Data.Enums
{
    public enum PermissionType
    {
        [Display(Name = "User administration - add, edit, deactivate users")]
        AddEditUser = 1,

        [Display(Name = "Add/Edit Organizer")]
        AddEditOrganizer = 2,

        [Display(Name = "Add/Edit Compaign")]
        AddEditCompaign = 3,

        [Display(Name = "Add/Edit Group")]
        AddEditGroup = 4,

        [Display(Name = "Invite Participants")]
        InviteParticipant = 5,

    }
}
