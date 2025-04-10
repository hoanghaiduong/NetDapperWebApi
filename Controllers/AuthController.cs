using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.DTO;
using NetDapperWebApi.Models;

namespace NetDapperWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger, IUserService userService, IAuthService authService)
        {
            _logger = logger;
            _userService = userService;
            _authService = authService;
        }
        //test api authorzation

        // [HttpGet,Authorize]
        // public async Task<IResult> Test(){
        //     try
        //     {
        //         var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //         var email=User.FindFirstValue(ClaimTypes.Email);
        //         return Results.Ok(new {
        //             message="Test",
        //             data=new {
        //                 email,
        //                 uid
        //             }
        //         });
        //     }
        //     catch (System.Exception)
        //     {

        //         throw;
        //     }
        // }


        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] AuthDTO dto)
        {
            try
            {
                var result = await _authService.SignInAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
        [HttpPost("signup")]
        public async Task<IResult> SignUp([FromBody] AuthDTO dto)
        {
            try
            {
                var result = await _authService.SignUpAsync(dto);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPost("refresh-token"), Authorize]
        public async Task<IResult> RefreshToken([FromBody] RefreshTokenModel dto)
        {
            try
            {
                var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(uid))
                {
                    return Results.Unauthorized();
                }
                var result = await _authService.RefreshToken(dto, uid);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}