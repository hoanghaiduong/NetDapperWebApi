using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetDapperWebApi.Common.Constants;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.DTO;
using NetDapperWebApi.DTO.Creates;
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
        public async Task<IResult> Create([FromForm] CreateUserDTO user)
        {
            try
            {
                var result = await _userService.CreateUser(user);
                return Results.Ok(new { result });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetById([FromRoute] int id, [FromQuery] int depth = 0)
        {
            try
            {
                var user = await _userService.GetUserById(id, depth);
                if (user == null) return Results.NotFound();
                return Results.Ok(user);
            }

            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    ex.Message
                });
            }
        }
        [HttpGet("get"), Authorize]
        public async Task<IActionResult> GetUserByToken()
        {
            try
            {
                var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userService.GetUserById(int.Parse(uid), 1);
                if (user == null)
                {
                    return Unauthorized();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet, Authorize(Roles = $"{AppRole.Admin},{AppRole.Employee}")]

        public async Task<IResult> GetAll([FromQuery] PaginationModel model)
        {
            try
            {
                var users = await _userService.GetAllUsers(model);
                return Results.Ok(users);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    ex.Message
                });
            }

        }

        [HttpPut("{id}")]
        public async Task<IResult> Update([FromRoute] int id, [FromForm] UpdateUserDTO user)
        {
            try
            {
                var result = await _userService.UpdateUser(id, user);
                return Results.Ok(new { result });
            }

            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                var result = await _userService.DeleteUser(id);
                return Results.Ok(new { result });
            }

            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    ex.Message
                });
            }

        }
    }
}