
using NetDapperWebApi.DTO;
using NetDapperWebApi.Entities;
using NetDapperWebApi.Models;
using NetDapperWebApi.Services;

namespace NetDapperWebApi.Common.Interfaces
{
    public interface IHotelService
    {
        Task<Hotel> CreateHotel(HotelDTO hotel);
        Task<HotelWithRooms> GetHotel(int id,int depth);
        Task<PaginatedResult<HotelWithRooms>> GetAllHotels(PaginationModel paginationModel);
        Task<Hotel> UpdateHotel(Hotel hotel);
        Task<bool> DeleteHotel(int id);
    }
}