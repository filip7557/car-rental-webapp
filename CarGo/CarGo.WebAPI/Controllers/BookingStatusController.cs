using Microsoft.AspNetCore.Mvc;
using CarGo.Service.Common;

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

        [HttpGet("bookingStatus")]
        public async Task<IActionResult> GetAllAsync()
        {
            var bookingStatus = await _service.GetAllAsync();

            if (bookingStatus == null)
                return BadRequest();

            return Ok(bookingStatus);
        }

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