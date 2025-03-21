using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetDapperWebApi.DTO.Creates;
using NetDapperWebApi.DTO.Updates;
using NetDapperWebApi.Entities;
using NetDapperWebApi.Models;

namespace NetDapperWebApi.Common.Interfaces
{
    public interface ICategoryDetailsService
    {
        Task<PaginatedResult<CategoryDetails>> GetAllCategoryDetailsAsync(PaginationModel dto);
        Task<CategoryDetails?> GetCategoryDetailsByIdAsync(int id,int depth);
        Task<CategoryDetails> CreateCategoryDetailsAsync(CreateCategoryDetailsDTO categoryDetails);
        Task<CategoryDetails?> UpdateCategoryDetailsAsync(int id, UpdateCategoryDetailsDTO categoryDetails);
        Task<bool> DeleteCategoryDetailsAsync(int id);
    }
}