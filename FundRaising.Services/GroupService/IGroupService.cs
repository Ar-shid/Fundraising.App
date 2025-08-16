using FundRaising.Data.Models;
using FundRaising.DTO.GroupModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Services.GroupService
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupViewModel>> GetAllAsync();
        Task<GroupViewModel> GetByIdAsync(int id);
        Task<Group> CreateAsync(GroupViewModel model, IFormFile? file);
        Task<bool> UpdateAsync(GroupViewModel group, IFormFile? newFile);
        Task<bool> DeleteAsync(int id);
    }
}
