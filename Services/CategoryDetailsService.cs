using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.DTO.Creates;
using NetDapperWebApi.DTO.Updates;
using NetDapperWebApi.Entities;

namespace NetDapperWebApi.Services
{
    public class CategoryDetailsService : ICategoryDetailsService
    {
        private readonly IDbConnection _db;

        public CategoryDetailsService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<CategoryDetails>> GetAllCategoryDetailsAsync()
        {
            var sql = "SELECT * FROM CategoryDetails";
            return await _db.QueryAsync<CategoryDetails>(sql);
        }

        public async Task<CategoryDetails?> GetCategoryDetailsByIdAsync(int id)
        {
            var sql = "SELECT * FROM CategoryDetails WHERE Id = @Id";
            return await _db.QueryFirstOrDefaultAsync<CategoryDetails>(sql, new { Id = id });
        }

        public async Task<CategoryDetails> CreateCategoryDetailsAsync(CreateCategoryDetailsDTO dto)
        {
            var sql = @"CategoryDetails_Create";
            var result = await _db.QueryFirstOrDefaultAsync<CategoryDetails>(sql, new
            {
                dto.CategoryId,
                dto.Name,
                dto.Description,
                dto.Icon
            },commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<CategoryDetails?> UpdateCategoryDetailsAsync(int id, UpdateCategoryDetailsDTO dto)
        {
            var sql = @"
                UPDATE CategoryDetails
                SET Name = @Name,
                    Description = @Description,
                    Price = @Price
                WHERE Id = @Id";

            var rowsAffected = await _db.ExecuteAsync(sql, new
            {
                Id = id,
                dto.Name,
                dto.Description,
                dto.Price
            });

            if (rowsAffected > 0)
            {
                return await GetCategoryDetailsByIdAsync(id);
            }

            return null;
        }

        public async Task<bool> DeleteCategoryDetailsAsync(int id)
        {
            var sql = "DELETE FROM CategoryDetails WHERE Id = @Id";
            var rowsAffected = await _db.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }
    }
}