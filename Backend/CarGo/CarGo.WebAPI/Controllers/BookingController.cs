﻿using CarGo.Common;
using CarGo.Model;
using CarGo.Service;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Security.Claims;

namespace CarGo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        public IBookService _service;

        public BookingController(IBookService service)
        {
            _service = service;
        }



        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetBookingsAsync(
            string orderBy = "Id",
            string sortOrder = "asc",
            int? pageNumber = null,
            int? rpp = null,
            bool? isActive = true,
            DateTime? startDate = null,
            DateTime? endDate = null,
            Guid? statusId = null,
            Guid? pickUpLocationId = null,
            Guid? dropOffLocationId = null,
            Guid? userId = null,
            Guid? companyVehicleId = null,
            string? bookingStatusName = null,
            string? locationAddress = null,
            string? vehicleMakeName = null,
            string? vehicleModelName = null,
            string? imageUrl = null,
            string? userRole = null,
            Guid? companyId = null
        )
        {
          
            try
            {
                var bookingFilter = new BookingFilter
                {
                    IsActive = isActive,
                    UserId = userId,
                    CompanyVehicleId = companyVehicleId,
                    StartDate = startDate,
                    EndDate = endDate,
                    StatusId = statusId,
                    PickUpLocationId = pickUpLocationId,
                    DropOffLocationId = dropOffLocationId,
                    BookingStatusName = bookingStatusName,
                    LocationAddress = locationAddress,
                    VehicleMakeName = vehicleMakeName,
                    VehicleModelName = vehicleModelName,
                    ImageUrl = imageUrl,
                    UserRole=userRole,
                    CompanyId = companyId,
                };

                var sorting = new BookingSorting
                {
                    OrderBy = orderBy,
                    SortOrder = sortOrder
                };

                var paging = new BookingPaging
                {
                    PageNumber = pageNumber,
                    Rpp = rpp
                };

                var bookings = await _service.GetAllBookingsAsync(sorting, paging, bookingFilter);

                return bookings.Count > 0 ? Ok(bookings) : NotFound("No available bookings");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error when fetching booking: {ex.Message}");
            }
        }

        [Authorize]
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

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteBooking(Guid id)
        {
            try
            {
                var existingBooking = _service.GetBookingByIdAsync(id);
                if (existingBooking == null)
                {
                    return NotFound($"The Booking with Id={id} you want to delete does not exist");
                }

                await _service.SoftDeleteBookingAsync(id);
                return Ok("Booking has been deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error when deleting booking: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddBookingAsync(Booking booking)
        {
            try
            {
                if (booking == null)
                {
                    return BadRequest("The Booking information is incorrect");
                }

                    booking.Id = Guid.NewGuid();

                await _service.AddBookingAsync(booking);
                return Ok("Booking added succesfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error when adding a Booking: {ex.Message}");
            }
        }

        [Authorize]
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
                if (existingBooking == null)
                {
                    return NotFound($"The Booking with Id={id} you want to update does not exist");
                }

                await _service.UpdateBookingAsync(id, updatedBooking);
                return Ok("Booking has been succesfully updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error when updating Booking {ex.Message}");
            }
        }



        [Authorize]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateBookingStatusAsync(Guid id)
        {
 

            try
            {
               
                var existingBooking = await _service.GetBookingByIdAsync(id);
                if (existingBooking == null)
                {
                    return NotFound($"The Booking with Id={id} you want to update does not exist");
                }

               
                await _service.UpdateBookingStatusAsync(id);
                return Ok("Booking status has been successfully updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error when updating Booking status {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("getTotalPrice")]
        public IActionResult GetTotalPrice(string startDate, string endDate, decimal dailyPrice)
        {
            try {
                var start = DateTime.Parse(startDate);
                var end = DateTime.Parse(endDate);
                Console.WriteLine("Days: " + (end - start).Days);
                var totalPrice = (end - start).Days * dailyPrice;
                Console.WriteLine(totalPrice);
                return Ok(totalPrice);
            } catch (Exception)
            {
                return Ok("Input data to calculate.");
            }
        }

    }
}