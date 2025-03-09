using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using FluentValidation;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.DTO;
using NetDapperWebApi.DTO.Creates;
using NetDapperWebApi.DTO.Updates;
using NetDapperWebApi.Entities;
using NetDapperWebApi.Models;

namespace NetDapperWebApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IDbConnection _db;

        public CategoryService(IDbConnection db)
        {
            _db = db;
        }

        // Method mới: Lấy cây danh mục kèm chi tiết sử dụng stored procedure
        private IEnumerable<CategoryTreeDto> BuildCategoryTree(IEnumerable<CategoryTreeDto> flatCategories)
        {
            // Tạo dictionary để tra cứu nhanh theo CategoryId
            var lookup = flatCategories.ToDictionary(c => c.CategoryId);
            var tree = new List<CategoryTreeDto>();

            foreach (var category in flatCategories)
            {
                // Nếu có ParentId, thêm vào danh sách Children của danh mục cha
                if (category.ParentId.HasValue && lookup.TryGetValue(category.ParentId.Value, out var parent))
                {
                    parent.Children.Add(category);
                }
                else
                {
                    // Nếu không có ParentId (là danh mục gốc) thì thêm vào danh sách cây
                    tree.Add(category);
                }
            }
            return tree;
        }

        public async Task<List<CategoryTreeDto>> GetCategoryTreeWithDetailsAsync(int? maxDepth)
        {
            using var multi = await _db.QueryMultipleAsync(
                "GetCategoryTreeWithDetails",
                new { MaxDepth = maxDepth },
                commandType: CommandType.StoredProcedure);

            // Result set 1: Danh sách Categories
            var categories = (await multi.ReadAsync<CategoryTreeDto>()).AsList();

            // Result set 2: Danh sách CategoryDetails
            var details = (await multi.ReadAsync<CategoryDetailsDto>()).AsList();

            // Ghép nối CategoryDetails vào Category theo CategoryId
            foreach (var category in categories)
            {
                category.Details = details.FindAll(d => d.CategoryId == category.CategoryId);
            }
            // Xây dựng cấu trúc cây từ danh sách flat
            var tree = BuildCategoryTree(categories);

            return tree.ToList();
        }

        public async Task<PaginatedResult<Category>> GetAllCategoriesAsync(PaginationModel dto)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@PageNumber", dto.PageNumber, DbType.Int32);
            parameters.Add("@PageSize", dto.PageSize, DbType.Int32);
            parameters.Add("@Depth", dto.Depth, DbType.Int32);
            parameters.Add("@Search", dto.Search ?? "", DbType.String);

            using var multi = await _db.QueryMultipleAsync(
                "Category_GetAll",
                parameters,
                commandType: CommandType.StoredProcedure);

            // Result set 1: Tổng số bản ghi (TotalCount) – có thể dùng để phân trang, ở đây ta chỉ đọc và bỏ qua.
            var totalCount = await multi.ReadFirstAsync<int>();

            // Result set 2: Danh sách Category (danh sách flat theo phân trang)
            var categories = (await multi.ReadAsync<Category>()).ToList();

            // Nếu Depth >= 1, lấy CategoryDetails cho các Category hiện có
            if (dto.Depth >= 1)
            {
                var details = (await multi.ReadAsync<CategoryDetails>()).ToList();
                foreach (var category in categories)
                {
                    category.Details = details.Where(d => d.CategoryId == category.Id).ToList();
                }
            }

            // // Nếu Depth >= 2, lấy danh sách Children cho các Category (nếu stored procedure trả về result set này)
            // if (dto.Depth >= 2)
            // {
            //     var children = (await multi.ReadAsync<Category>()).ToList();
            //     // Gán các category con vào property Children của category cha
            //     // Giả sử các bản ghi children này có ParentId được gán
            //     var lookup = categories.ToDictionary(c => c.Id);
            //     foreach (var child in children)
            //     {
            //         if (child.ParentId.HasValue && lookup.ContainsKey(child.ParentId.Value))
            //         {
            //             lookup[child.ParentId.Value].Children.Add(child);
            //         }
            //     }
            // }

            return new PaginatedResult<Category>(categories, totalCount, currentPage: dto.PageNumber, pageSize: dto.PageSize);
        }

        public async Task<Category?> GetCategoryByIdAsync(int categoryId, int depth)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", categoryId, DbType.Int32);
            parameters.Add("@Depth", depth, DbType.Int32);

            using var multi = await _db.QueryMultipleAsync(
                "Category_GetById",
                parameters,
                commandType: CommandType.StoredProcedure);

            // Result set 1: Thông tin của Category
            var category = await multi.ReadFirstOrDefaultAsync<Category>();
            if (category == null)
                return null;

            // Nếu Depth >= 1, lấy CategoryDetails cho Category
            if (depth >= 1)
            {
                var details = (await multi.ReadAsync<CategoryDetails>()).ToList();
                category.Details = details;
            }

            // Nếu Depth >= 2, lấy danh sách Children (Category con) cho Category
            if (depth >= 2)
            {
                var children = (await multi.ReadAsync<Category>()).ToList();
                category.Children = children;
            }

            return category;
        }

        public async Task<Category> CreateCategoryAsync(CreateCategoryDTO dto)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ParentId", dto.ParentId == 0 ? DBNull.Value : dto.ParentId, DbType.Int32);
                parameters.Add("@Name", dto.Name, DbType.String);


                var category = await _db.QueryFirstOrDefaultAsync<Category>(
                    "Category_Create",
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

            var result = await _db.QueryFirstOrDefaultAsync<Category>("Category_Update", new { id, category.Name, category.ParentId }, commandType: CommandType.StoredProcedure);


            return result;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {

            var rows = await _db.ExecuteAsync("Category_Delete", new { Id = categoryId }, commandType: CommandType.StoredProcedure);
            return rows != -1;
        }
    }
}