using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetDapperWebApi.Entities;

namespace NetDapperWebApi.Common.Interfaces
{
    public interface IRoomTypeService
    {
        Task<RoomType> CreateRoomType(RoomType roomType);
        Task<RoomType> GetRoomType(int id);
        Task<IEnumerable<RoomType>> GetRoomTypes();
        Task<RoomType> UpdateRoomType(int id, RoomType roomType);
        Task<bool> DeleteRoomType(int id);
    }
}