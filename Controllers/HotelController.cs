using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.DTO;
using NetDapperWebApi.Entities;
using NetDapperWebApi.Models;

namespace NetDapperWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly ILogger<HotelController> _logger;

        public HotelController(IHotelService hotelService, ILogger<HotelController> logger)
        {
            _hotelService = hotelService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HotelDTO hotel)
        {
            var result = await _hotelService.CreateHotel(hotel);
            return Ok(new { message = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] int depth = 0)
        {
            try
            {
                var hotel = await _hotelService.GetHotel(id, depth);
                if (hotel == null) return NotFound();
                return Ok(hotel);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationModel paginationModel)
        {
            try
            {
                var hotels = await _hotelService.GetAllHotels(paginationModel);
                return Ok(new
                {
                    data = hotels
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Hotel hotel)
        {
            if (id != hotel.Id) return BadRequest();
            var result = await _hotelService.UpdateHotel(hotel);
            return Ok(new { message = result });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _hotelService.DeleteHotel(id);
            return Ok(new { message = result });
        }
    }
}