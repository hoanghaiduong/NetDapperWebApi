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
        Task<RoomType> CreateRoomType(RoomType roomType);
        Task<RoomType> GetRoomType(int id);
        Task<PaginatedResult<RoomTypeDTOWithRooms>> GetRoomTypes(PaginationModel pagination);
        Task<RoomType> UpdateRoomType(int id, RoomType roomType);
        Task<bool> DeleteRoomType(int id);
        // Task<RoomTypeDTOWithRoom> GetRoomTypeWithRooms(int id);
        Task<RoomTypeDTOWithRooms> GetRoomTypeWithRooms(int id, int depth);
    }
}