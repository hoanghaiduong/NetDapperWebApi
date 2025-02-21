
using NetDapperWebApi.DTO;
using NetDapperWebApi.Entities;
using NetDapperWebApi.Models;
using NetDapperWebApi.Services;

namespace NetDapperWebApi.Common.Interfaces
{
    public interface IHotelService
    {
        Task<Hotel> CreateHotel(HotelDTO hotel);
        Task<Hotel> GetHotel(int id, int depth);
        Task<PaginatedResult<Hotel>> GetAllHotels(PaginationModel paginationModel);
        Task<Hotel> UpdateHotel(Hotel hotel);
        Task<bool> DeleteHotel(int id);
    }
}