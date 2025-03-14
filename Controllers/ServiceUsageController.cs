using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.DTO.Creates;
using NetDapperWebApi.Entities;

namespace NetDapperWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceUsageController : ControllerBase
    {
        private readonly IServiceUsageService _serviceUsageService;

        public ServiceUsageController(IServiceUsageService serviceUsageService)
        {
            _serviceUsageService = serviceUsageService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceUsageDTO serviceUsage)
        {
            try
            {
                if (serviceUsage == null)
                    return BadRequest("Invalid request data");

                var result = await _serviceUsageService.CreateServiceUsageAsync(serviceUsage);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });

            }
        }

        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetAllByBookingId(int bookingId)
        {
            var result = await _serviceUsageService.GetAllByBookingIdAsync(bookingId);
            return Ok(result);
        }
    }
}