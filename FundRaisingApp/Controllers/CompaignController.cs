using FundRaising.Data.Models;
using FundRaising.DTO.CompaignModels;
using FundRaising.Services.CompaignService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FundRaisingApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CompaignController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICompaignService _compaignService;

        public CompaignController(ICompaignService compaignService,
                              IConfiguration configuration)
        {
            _compaignService = compaignService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var compaigns = await _compaignService.GetAllAsync();
            return Ok(compaigns);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var compaign = await _compaignService.GetByIdAsync(id);
            if (compaign == null) return NotFound();
            return Ok(compaign);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CompaignViewModel dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            dto.CreatedByName = userId;
            var result = await _compaignService.CreateAsync(dto, dto.UploadImages);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CompaignViewModel dto)
        {
            var updated = await _compaignService.UpdateAsync(dto, dto.UploadImages);
            if (!updated) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _compaignService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return Ok(deleted);
        }


    }

}
