using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetDapperWebApi.DTO;
using NetDapperWebApi.Entities;
using NetDapperWebApi.Models;
using NetDapperWebApi.Services;

namespace NetDapperWebApi.Common.Interfaces
{
    public interface IRoomTypeService
    {
        Task<RoomType> CreateRoomType(RoomTypeDTO roomType);
        Task<RoomType> GetRoomType(int id);
        Task<PaginatedResult<RoomType>> GetRoomTypes(PaginationModel pagination);
        Task<RoomType> UpdateRoomType(RoomTypeDTO roomType);
        Task<bool> DeleteRoomType(int id);
        // Task<RoomTypeDTOWithRoom> GetRoomTypeWithRooms(int id);
        Task<RoomType> GetRoomTypeWithRooms(int id, int depth);
    }
}