using CarGo.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarGoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleModelController : ControllerBase
    {
        private IVehicleModelService _service;

        public VehicleModelController(IVehicleModelService carGoService)
        {
            _service = carGoService;
        }

        [Authorize(Roles = "User,Manager,Administrator")]
        [HttpGet("vehicleModels")]
        public async Task<IActionResult> GetAllAsync()
        {
            var vehicleModel = await _service.GetAllAsync();

            if (vehicleModel == null)
                return BadRequest();

            return Ok(vehicleModel);
        }

        [Authorize(Roles = "User,Manager,Administrator")]
        [HttpGet("{vehicleModelId}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var vehicleModelId = await _service.GetByIdAsync(id);

            if (vehicleModelId == null)
                return BadRequest();

            return Ok(vehicleModelId);
        }
    }
}