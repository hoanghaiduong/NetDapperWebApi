using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
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