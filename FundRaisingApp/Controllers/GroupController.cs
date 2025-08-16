using FundRaising.Data.Models;
using FundRaising.DTO.GroupModels;
using FundRaising.Services.GroupService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FundRaisingApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService,
                              IConfiguration configuration)
        {
            _groupService = groupService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var groups = await _groupService.GetAllAsync();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var group = await _groupService.GetByIdAsync(id);
            if (group == null) return NotFound();
            return Ok(group);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] GroupViewModel dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            dto.CreatedByName = userId;
            var result = await _groupService.CreateAsync(dto, dto.UploadImage);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] GroupViewModel dto)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            dto.UpdatedByName = userId;
            var updated = await _groupService.UpdateAsync(dto, dto.UploadImage);
            if (!updated) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _groupService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return Ok(deleted);
        }


    }

}
