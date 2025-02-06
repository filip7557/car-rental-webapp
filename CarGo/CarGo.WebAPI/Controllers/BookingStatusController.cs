using CarGo.Service.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CarGoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingStatusController : ControllerBase
    {
        private IBookingStatusService _service;

        public BookingStatusController(IBookingStatusService carGoService)
        {
            _service = carGoService;
        }

        [Authorize(Roles = "User,Manager,Administrator")]
        [HttpGet("bookingStatus")]
        public async Task<IActionResult> GetAllAsync()
        {
            var bookingStatus = await _service.GetAllAsync();

            if (bookingStatus == null)
                return BadRequest();

            return Ok(bookingStatus);
        }

        [Authorize(Roles = "User,Manager,Administrator")]
        [HttpGet("{bookingStatus-id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var bookingStatusId = await _service.GetByIdAsync(id);

            if (bookingStatusId == null)
                return BadRequest();

            return Ok(bookingStatusId);
        }
    }
}