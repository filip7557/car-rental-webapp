using CarGo.Model;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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


        [HttpGet("company/{companyId}/locations")]
        [Authorize(Roles = "User,Administrator,Manager")]
        public async Task<IActionResult> GetLocationsByCompanyId(Guid companyId)
        {
            try
            {
                var locations = await _locationService.GetLocationsByCompanyIdAsync(companyId);

                if (locations == null || locations.Count == 0)
                {
                    return NotFound("No active locations found for this company.");
                }

                return Ok(locations);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
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

        [HttpPost("{companyId}")]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Post(Guid companyId, Location location)
        {
            try
            {
                if (location != null)
                {
                    var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                    await _locationService.PostAsync(companyId, location);

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
                var location = await _locationService.GetByIdAsync(locationId);
                if (location != null)
                {
                    var userId =
                        Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!
                            .Value); //(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                    await _locationService.DeleteAsync(locationId);
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