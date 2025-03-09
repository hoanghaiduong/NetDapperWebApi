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
using NetDapperWebApi.Models;

namespace NetDapperWebApi.Services
{
    public class CategoryDetailsService : ICategoryDetailsService
    {
        private readonly IDbConnection _db;

        public CategoryDetailsService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<PaginatedResult<CategoryDetails>> GetAllCategoryDetailsAsync(PaginationModel dto)
        {
            var parameters = new
            {
                PageNumber = dto.PageNumber,
                PageSize = dto.PageSize,
                Depth = dto.Depth,
                Search = dto.Search
                
            };

            using var multi = await _db.QueryMultipleAsync("CategoryDetails_GetAll", parameters, commandType: CommandType.StoredProcedure);

            // Lấy tổng số bản ghi
            var totalCount = await multi.ReadSingleAsync<int>();

            // Lấy danh sách CategoryDetails
            var categoryDetails = (await multi.ReadAsync<CategoryDetails>()).ToList();

            if (dto.Depth >= 1)
            {
                // Lấy danh sách Category
                var categories = (await multi.ReadAsync<Category>()).ToList();

                foreach (var detail in categoryDetails)
                {
                    detail.Category = categories.FirstOrDefault(c => c.Id == detail.CategoryId);
                }
            }

            return new PaginatedResult<CategoryDetails>(categoryDetails, totalCount, dto.PageNumber, dto.PageSize);
        }

        public async Task<CategoryDetails?> GetCategoryDetailsByIdAsync(int id, int depth)
        {
            var parameters = new { Id = id, Depth = depth };

            using var multi = await _db.QueryMultipleAsync("CategoryDetails_GetById", parameters, commandType: CommandType.StoredProcedure);

            var categoryDetail = await multi.ReadSingleOrDefaultAsync<CategoryDetails>();
            if (categoryDetail == null) return null;

            if (depth >= 1)
            {
                var category = await multi.ReadSingleOrDefaultAsync<Category>();
                categoryDetail.Category = category;
            }

            return categoryDetail;
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
            }, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<CategoryDetails?> UpdateCategoryDetailsAsync(int id, UpdateCategoryDetailsDTO dto)
        {
            var sql = @"CategoryDetails_Update";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            parameters.Add("@CategoryId", dto.CategoryId);
            parameters.Add("@Name", dto.Name);
            parameters.Add("@Description", dto.Description);
            parameters.Add("@Icon", dto.Icon);

            var result = await _db.QueryFirstOrDefaultAsync<CategoryDetails>(sql, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<bool> DeleteCategoryDetailsAsync(int id)
        {
            var sql = "DELETE FROM CategoryDetails WHERE Id = @Id";
            var rowsAffected = await _db.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }
    }
}