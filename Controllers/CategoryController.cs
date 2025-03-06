using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.DTO;
using NetDapperWebApi.DTO.Creates;
using NetDapperWebApi.DTO.Updates;
using NetDapperWebApi.Entities;
using NetDapperWebApi.Models;

namespace NetDapperWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // Endpoint mới: Lấy cây danh mục kèm chi tiết dưới dạng kiểu dữ liệu (DTO)
        [HttpGet("tree/details")]
        public async Task<IActionResult> GetCategoryTreeWithDetails([FromQuery] int? maxDepth)
        {
            var result = await _categoryService.GetCategoryTreeWithDetailsAsync(maxDepth);
            return Ok(result);
        }
        private Category BuildTree(Category parent, IEnumerable<Category> allCategories)
        {
            parent.Children = allCategories.Where(c => c.ParentId == parent.Id).ToList();
            foreach (var child in parent.Children)
            {
                BuildTree(child, allCategories);
            }
            return parent;
        }

        [HttpGet("tree")]
        public async Task<IActionResult> GetTree([FromQuery] PaginationModel dto)
        {
            var results = await _categoryService.GetAllCategoriesAsync(dto);
            var items = results.Items
                .Where(c => c.ParentId == null)
                .Select(c => BuildTree(c, results.Items))
                .ToList();
            results.Items = items;
            return Ok(results);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] PaginationModel dto)
        {
            var categories = await _categoryService.GetAllCategoriesAsync(dto);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id, [FromQuery] int depth = 0)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id, depth);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO category)
        {
            var result = await _categoryService.CreateCategoryAsync(category);
            return Ok(new { result });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryDTO category)
        {

            var result = await _categoryService.UpdateCategoryAsync(id, category);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok(new
            {
                message = $"Xoá danh mục với id = {id} thành công !"
            });
        }


    }
}