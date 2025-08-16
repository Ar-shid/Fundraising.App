using AutoMapper;
using FundRaising.Common.Mappings;
using FundRaising.Data.Enums;
using FundRaising.Data.Models;
using FundRaising.DTO.CompaignModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.DTO.GroupModels
{
    public class GroupViewModel: IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? LogoPath { get; set; }
        public GroupStatus Status { get; set; }
        public bool isApproved { get; set; }
        public bool isDraft { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedByName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedByName { get; set; }

        [NotMapped]
        public IFormFile? UploadImage { get; set; }
        public ICollection<GroupMemberViewModel> GroupMembers { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Group, GroupViewModel>()
                .ForMember(u => u.CreatedByName, y => y.MapFrom(u => u.CreatedBy.FirstName + " " + u.CreatedBy.LastName))
                .ForMember(u => u.UpdatedByName, y => y.MapFrom(u => u.UpdatedBy == null ? "" : (u.UpdatedBy.FirstName + " " + u.UpdatedBy.LastName)))
                .ForMember(u => u.GroupMembers, y => y.MapFrom(u => u.GroupMembers));
            configuration.CreateMap<GroupViewModel, Group>()
               .ForMember(u => u.GroupMembers, y => y.Ignore());

        }
    }
}
