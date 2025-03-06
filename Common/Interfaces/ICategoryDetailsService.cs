using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetDapperWebApi.DTO.Creates;
using NetDapperWebApi.DTO.Updates;
using NetDapperWebApi.Entities;

namespace NetDapperWebApi.Common.Interfaces
{
    public interface ICategoryDetailsService
    {
        Task<IEnumerable<CategoryDetails>> GetAllCategoryDetailsAsync();
        Task<CategoryDetails?> GetCategoryDetailsByIdAsync(int id);
        Task<CategoryDetails> CreateCategoryDetailsAsync(CreateCategoryDetailsDTO categoryDetails);
        Task<CategoryDetails?> UpdateCategoryDetailsAsync(int id, UpdateCategoryDetailsDTO categoryDetails);
        Task<bool> DeleteCategoryDetailsAsync(int id);
    }
}