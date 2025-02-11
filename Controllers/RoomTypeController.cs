using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.Entities;
using NetDapperWebApi.Services;

namespace NetDapperWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeService _roomTypeService;
        private readonly ILogger<RoomTypeController> _logger;

        public RoomTypeController(IRoomTypeService roomTypeService, ILogger<RoomTypeController> logger)
        {
            _roomTypeService = roomTypeService;
            _logger = logger;
        }

        // ✅ Tạo RoomType (201 Created)
        [HttpPost]
        public async Task<IResult> CreateRoomType([FromBody] RoomType roomType)
        {
            try
            {
                var result = await _roomTypeService.CreateRoomType(roomType);
                if (result == null)
                {
                    return Results.BadRequest();
                }
                return Results.Ok(new
                {
                    data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating RoomType");
                return Results.BadRequest(new { ex.Message });
            }
        }

        // ✅ Lấy RoomType theo ID
        [HttpGet("{id:int}")]
        public async Task<IResult> GetRoomTypeById(int id)
        {
            var roomType = await _roomTypeService.GetRoomType(id);
            return roomType == null
                ? Results.NotFound(new { message = "RoomType not found" })
                : Results.Ok(roomType);
        }

        // ✅ Lấy danh sách RoomType (Có phân trang)
        [HttpGet]
        public async Task<IResult> GetAllRoomTypes()
        {
            var result = await _roomTypeService.GetRoomTypes();
            return Results.Ok(result);
        }

        // ✅ Cập nhật RoomType (204 No Content)
        [HttpPut("{id:int}")]
        public async Task<IResult> UpdateRoomType(int id, [FromBody] RoomType roomType)
        {
            roomType.Id = id;
            var success = await _roomTypeService.UpdateRoomType(id, roomType);
            return success != null
                ? Results.Ok(new
                {
                    data = success
                })
                : Results.NotFound(new { message = "RoomType not found" });
        }

        // ✅ Xóa RoomType (204 No Content)
        [HttpDelete("{id:int}")]
        public async Task<IResult> DeleteRoomType(int id)
        {
            var success = await _roomTypeService.DeleteRoomType(id);
            return success
                ? Results.NoContent()
                : Results.NotFound(new { message = "RoomType not found" });
        }
    }
}