using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FluentValidation;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.DTO.Creates;
using NetDapperWebApi.DTO.Updates;
using NetDapperWebApi.Entities;

namespace NetDapperWebApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IDbConnection _db;

        public CategoryService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {

            return await _db.QueryAsync<Category>("SELECT * FROM Category");
        }

        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {

            return await _db.QueryFirstOrDefaultAsync<Category>("SELECT * FROM Category WHERE Id = @Id", new { Id = categoryId });
        }

        public async Task<Category> CreateCategoryAsync(CreateCategoryDTO dto)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ParentId", dto.ParentId == 0 ? DBNull.Value : dto.ParentId, DbType.Int32);
                parameters.Add("@Name", dto.Name, DbType.String);


                var category = await _db.QueryFirstOrDefaultAsync<Category>(
                    "CreateCategory",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return category ?? throw new Exception("Không thể tạo danh mục.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tạo danh mục: {ex.Message}");
            }
        }


        public async Task<Category> UpdateCategoryAsync(int id, UpdateCategoryDTO category)
        {

            var result = await _db.QueryFirstOrDefaultAsync<Category>("UpdateCategory", new { id, category.Name, category.ParentId }, commandType: CommandType.StoredProcedure);
            return result ?? throw new Exception();
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {

            var rows = await _db.ExecuteAsync("DeleteCategory", new { Id = categoryId }, commandType: CommandType.StoredProcedure);
            return rows == -1;
        }
    }
}