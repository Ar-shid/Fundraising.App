using FundRaising.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FundRaising.Common.ModelConstants;
using FundRaising.Data.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;

namespace FundRaising.DTO.CompaignModels
{
    public class CompaignViewModel : IMapFrom<Compaign>, IHaveCustomMappings
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(UserConstants.NameMaxlength)]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? RequiredAmount { get; set; }
        public decimal? RaisedAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Status { get; set; }
        public bool IsCompaignDeleted { get; set; }
        public bool IsCompaignLaunch { get; set; }
        public bool IsApproved { get; set; }
        public string? AssignedToId { get; set; }
        public string? AssignedToName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedByName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedByName { get; set; }
        [NotMapped]
        public IFormFileCollection? UploadImages { get; set; }
        public List<string> ImagePaths { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Compaign, CompaignViewModel>()
                .ForMember(u => u.UploadImages, y => y.Ignore())
                .ForMember(u => u.CreatedByName, y => y.MapFrom(u => u.CreatedBy.FirstName + " "+ u.CreatedBy.LastName))
                .ForMember(u => u.UpdatedByName, y => y.MapFrom(u => u.UpdatedBy == null ? "" : (u.UpdatedBy.FirstName + " "+ u.UpdatedBy.LastName)))
                .ForMember(u => u.AssignedToName, y => y.MapFrom(u => u.AssignedTo == null ? "" : (u.AssignedTo.FirstName + " "+ u.AssignedTo.LastName)))
                .ForMember(u => u.ImagePaths, y => y.MapFrom(u => u.Images.Select(c => c.Image)));
            configuration.CreateMap<CompaignViewModel, Compaign>()
               .ForMember(u => u.Images, y => y.Ignore())
               .ForMember(u => u.AssignedToId, y => y.MapFrom(u => u.AssignedToId));

        }
    }
}
