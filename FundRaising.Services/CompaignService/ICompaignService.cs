using FundRaising.Data.Models;
using FundRaising.DTO.CompaignModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Services.CompaignService
{
    public interface ICompaignService
    {
        Task<IEnumerable<CompaignViewModel>> GetAllAsync();
        Task<CompaignViewModel> GetByIdAsync(int id);
        Task<Compaign> CreateAsync(CompaignViewModel model, IFormFileCollection? files);
        Task<bool> UpdateAsync(CompaignViewModel compaign, IFormFileCollection? newFiles);
        Task<bool> DeleteAsync(int id);
    }
}
