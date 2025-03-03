
using NetDapperWebApi.DTO;
using NetDapperWebApi.DTO.Creates;
using NetDapperWebApi.DTO.Updates;
using NetDapperWebApi.Entities;
using NetDapperWebApi.Models;
using NetDapperWebApi.Services;

namespace NetDapperWebApi.Common.Interfaces
{
    public interface IHotelService
    {
        Task<Hotel> CreateHotel(CreateHotelDTO hotel);
        Task<Hotel> GetHotel(int id, int depth);
        Task<PaginatedResult<Hotel>> GetAllHotels(PaginationModel paginationModel);
        Task<Hotel> UpdateHotel(int id,UpdateHotelDTO hotel);
        Task<bool> DeleteHotel(int id);
    }
}