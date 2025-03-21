using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.DTO.Creates;
using NetDapperWebApi.DTO.Updates;
using NetDapperWebApi.Models;

namespace NetDapperWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryDetailsController : ControllerBase
    {
        private readonly ICategoryDetailsService _categoryDetailsService;

        public CategoryDetailsController(ICategoryDetailsService categoryDetailsService)
        {
            _categoryDetailsService = categoryDetailsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoryDetails([FromQuery] PaginationModel dto)
        {
            var categoryDetails = await _categoryDetailsService.GetAllCategoryDetailsAsync(dto);
            return Ok(categoryDetails);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryDetailsById([FromRoute] int id, [FromQuery] int depth = 0)
        {
            var categoryDetails = await _categoryDetailsService.GetCategoryDetailsByIdAsync(id, depth);
            if (categoryDetails == null) return NotFound();
            return Ok(categoryDetails);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryDetails([FromBody] CreateCategoryDetailsDTO dto)
        {
            var createdCategoryDetails = await _categoryDetailsService.CreateCategoryDetailsAsync(dto);
            return CreatedAtAction(nameof(GetCategoryDetailsById), new { id = createdCategoryDetails.Id }, createdCategoryDetails);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryDetails(int id, [FromBody] UpdateCategoryDetailsDTO dto)
        {
            var updatedCategoryDetails = await _categoryDetailsService.UpdateCategoryDetailsAsync(id, dto);
            if (updatedCategoryDetails == null) return NotFound();
            return NoContent();
        }
    }
}