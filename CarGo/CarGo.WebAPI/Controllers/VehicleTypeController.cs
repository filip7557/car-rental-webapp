using Microsoft.AspNetCore.Mvc;
using CarGo.Service.Common;

namespace CarGoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypeController : ControllerBase
    {
        private IVehicleTypeService _service;

        public VehicleTypeController(IVehicleTypeService carGoService)
        {
            _service = carGoService;
        }

        [HttpGet("vehicleTypes")]
        public async Task<IActionResult> GetAllAsync()
        {
            var vehicleTypes = await _service.GetAllAsync();

            if (vehicleTypes == null)
                return BadRequest();

            return Ok(vehicleTypes);
        }

        [HttpGet("vehicleTypesId")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var vehicleTypesId = await _service.GetByIdAsync(id);

            if (vehicleTypesId == null)
                return BadRequest();

            return Ok(vehicleTypesId);
        }
    }
}