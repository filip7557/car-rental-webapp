using System.Security.Claims;
using CarGo.Model;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarGo.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService location)
        {
            _locationService = location;
        }

        [HttpGet]
        [Authorize(Roles = "User,Administrator,Manager")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var locations = await _locationService.GetAsync();
                return Ok(locations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User,Administrator,Manager")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var locations = await _locationService.GetByIdAsync(id);
                if (locations == null)
                {
                    return NotFound("Location not found with the provided ID.");
                }
                return Ok(locations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Post(Location location)
        {
            try
            {
                if (location != null)
                {
                    var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                    await _locationService.PostAsync(location, userId);

                    return Ok("Location of company added successfully.");
                }
                return BadRequest("Failed to add location of company.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        

        [HttpDelete("{locationId}")]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> DeleteAsync(Guid locationId)
        {
            try
            {
                var location =  await _locationService.GetByIdAsync(locationId);
                if (location != null)
                {
                    var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value); //(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                    await _locationService.DeleteAsync(locationId, userId);
                    return Ok("Location isActive status changed successfully.");
                }
                return NotFound("Location not found with the provided ID.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}