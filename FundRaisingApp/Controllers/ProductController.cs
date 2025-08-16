using FundRaising.Data.Models;
using FundRaising.DTO.ProductModels;
using FundRaising.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FundRaisingApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IProductService _productService;

        public ProductController(IProductService productService,
                              IConfiguration configuration)
        {
            _productService = productService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductViewModel dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            dto.CreatedByName = userId;
            var result = await _productService.CreateAsync(dto, dto.UploadImages);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ProductViewModel dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            dto.UpdatedByName = userId;
            var updated = await _productService.UpdateAsync(dto, dto.UploadImages);
            if (!updated) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return Ok(deleted);
        }


    }

}
