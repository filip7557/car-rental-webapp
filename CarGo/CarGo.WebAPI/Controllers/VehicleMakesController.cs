using CarGo.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace CarGoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMakesController : ControllerBase
    {
        private IVehicleMakeService _service;

        public VehicleMakesController(IVehicleMakeService carGoService)
        {
            _service = carGoService;
        }

        [HttpGet("vehicleMakes")]
        public async Task<IActionResult> GetAllAsync()
        {
            var vehicleMakes = await _service.GetAllAsync();

            if (vehicleMakes == null)
                return BadRequest();

            return Ok(vehicleMakes);
        }

        [HttpGet("{vehicleMakesId}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var vehicleMakesId = await _service.GetByIdAsync(id);

            if (vehicleMakesId == null)
                return BadRequest();

            return Ok(vehicleMakesId);
        }
    }
}