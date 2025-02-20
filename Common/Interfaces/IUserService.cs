

using NetDapperWebApi.DTO;
using NetDapperWebApi.Entities;
using NetDapperWebApi.Models;
using NetDapperWebApi.Services;

namespace NetDapperWebApi.Common.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(User user);
        Task<UserRelations> GetUserById(int id,int depth);
        Task<PaginatedResult<UserRelations>> GetAllUsers(PaginationModel pagination);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
    }
}