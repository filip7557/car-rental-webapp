using CarGo.Model;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace CarGo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        public IBookService _service;
        public BookingController(IBookService service) {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetBookingsAsync()
        {
            try
            {
                var bookings = await _service.GetAllBookingsAsync();

                return bookings.Count > 0 ? Ok(bookings) : NotFound("No available bookings");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error when fetching bookings: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingByIdAsync(Guid id)
        {
            try
            {
                var booking = await _service.GetBookingByIdAsync(id);

                return booking != null ? Ok(booking) : NotFound("No available bookings");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error when fetching booking: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteBooking(Guid id)
        {
            try
            {
                var existingBooking = _service.GetBookingByIdAsync(id);
                if (existingBooking == null)
                {
                    return NotFound($"The Booking with Id={id} you want to delete does not exist");
                }

                await _service.DeleteBookingAsync(id);
                return Ok("Booking has been deleted");
            }
            catch(Exception ex) 
            { 
            return StatusCode(500, $"Error when deleting booking: {ex.Message}");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> AddBookingAsync(Booking booking)
        {
            try
            {
                if (booking == null)
                {
                    return BadRequest("The Booking information is incorrect");
                }
                await _service.AddBookingAsync(booking);
                return Ok("Booking added succesfully");
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error when adding a Booking: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookingAsync(Guid id, Booking updatedBooking)
        {

            if (updatedBooking == null)
            {
                return BadRequest("The Booking information is incorrect");
            }

            try
            {
                var existingBooking = _service.GetBookingByIdAsync(id);
                if(existingBooking== null)
                {
                    return NotFound($"The Booking with Id={id} you want to delete does not exist");
                }
                await _service.UpdateBookingAsync(id, updatedBooking);
                return Ok("Booking has been succesfully updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error when updating Booking {ex.Message}");
            }
        }


    }

}

