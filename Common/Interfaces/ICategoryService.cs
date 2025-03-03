using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetDapperWebApi.DTO.Creates;
using NetDapperWebApi.DTO.Updates;
using NetDapperWebApi.Entities;

namespace NetDapperWebApi.Common.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int categoryId);
        Task<Category> CreateCategoryAsync(CreateCategoryDTO category);
        Task<Category> UpdateCategoryAsync(int id,UpdateCategoryDTO category);
        Task<bool> DeleteCategoryAsync(int categoryId);
    }
}