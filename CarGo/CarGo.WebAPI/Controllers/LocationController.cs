using CarGo.Model;
using CarGo.Service.Common;
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
        [HttpPost("{id}")]
        public async Task<IActionResult> Post(Location location, Guid id)
        {
            try
            {
                if (location != null)
                {
                    await _locationService.PostAsync(location, id);

                    return Ok("Location of company added successfully.");
                }
                return BadRequest("Failed to add location of company.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    await _locationService.DeleteAsync(id);
                    return Ok("Location deleted successfully.");
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
