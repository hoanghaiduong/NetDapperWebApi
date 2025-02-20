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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            var result = await _userService.CreateUser(user);
            return Ok(new { message = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id,[FromQuery] int depth=0)
        {
            var user = await _userService.GetUserById(id,depth);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationModel model)
        {
            var users = await _userService.GetAllUsers(model);
            return Ok(users);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] User user)
        {
          
            var result = await _userService.UpdateUser(user);
            return Ok(new { message = result });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteUser(id);
            return Ok(new { message = result });
        }
    }
}