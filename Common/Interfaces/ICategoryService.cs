using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetDapperWebApi.DTO;
using NetDapperWebApi.DTO.Creates;
using NetDapperWebApi.DTO.Updates;
using NetDapperWebApi.Entities;
using NetDapperWebApi.Models;

namespace NetDapperWebApi.Common.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryTreeDto>> GetCategoryTreeWithDetailsAsync(int? maxDepth);
        Task<PaginatedResult<Category>> GetAllCategoriesAsync(PaginationModel pagination);
        Task<Category> GetCategoryByIdAsync(int categoryId,int depth);
        Task<Category> CreateCategoryAsync(CreateCategoryDTO category);
        Task<Category> UpdateCategoryAsync(int id, UpdateCategoryDTO category);
        Task<bool> DeleteCategoryAsync(int categoryId);

    }
}