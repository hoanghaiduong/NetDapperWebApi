
using Microsoft.AspNetCore.Mvc;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.Entities;


namespace NetDapperWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost]
        public async Task<IResult> Create([FromBody] Room dto)
        {
            var result = await _roomService.CreateRoom(dto);
            return Results.Ok(new { message = result });
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetById(int id)
        {
            var room = await _roomService.GetRoom(id);
            if (room == null) return Results.NotFound();
            return Results.Ok(room);
        }

        [HttpGet]
        public async Task<IResult> GetAll()
        {
            var rooms = await _roomService.GetRooms();
            return Results.Ok(rooms);
        }

        [HttpPut("{id}")]
        public async Task<IResult> Update(int id, [FromBody] Room room)
        {
            var roomResult = await _roomService.GetRoom(id);
            if (roomResult == null) return Results.NotFound();
            var result = await _roomService.UpdateRoom(id, room);
            return Results.Ok(new { message = result });
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            var result = await _roomService.DeleteRoom(id);
            return Results.Ok(new { message = result });
        }
    }
}