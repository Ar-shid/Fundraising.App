using AutoMapper;
using FundRaising.Common.Mappings;
using FundRaising.Data.Models;
using FundRaising.DTO.CompaignModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.DTO.GroupModels
{
    public class GroupMemberViewModel: IHaveCustomMappings
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<GroupMembers, GroupMemberViewModel>()
                .ForMember(u => u.GroupName, y => y.MapFrom(u => u.Group.Name))
                .ForMember(u => u.MemberName, y => y.MapFrom(u => u.Member == null ? "" : (u.Member.FirstName + " " + u.Member.LastName)));
            configuration.CreateMap<GroupMemberViewModel, GroupMembers>();

        }
    }
}
