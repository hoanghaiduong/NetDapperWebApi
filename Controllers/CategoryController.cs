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
        public async Task<IActionResult> GetTree()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var tree = categories
                .Where(c => c.ParentId == null)
                .Select(c => BuildTree(c, categories))
                .ToList();

            return Ok(tree);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO category)
        {
            var result = await _categoryService.CreateCategoryAsync(category);
            return Ok(new {result});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute]int id, [FromBody] UpdateCategoryDTO category)
        {
        
            var result = await _categoryService.UpdateCategoryAsync(id,category);
            if (result == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }


    }
}