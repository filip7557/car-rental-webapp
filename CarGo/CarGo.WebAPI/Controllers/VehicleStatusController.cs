using Microsoft.AspNetCore.Mvc;
using CarGo.Service.Common;

namespace CarGoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleStatusController : ControllerBase
    {
        private IVehicleStatusService _service;

        public VehicleStatusController(IVehicleStatusService carGoService)
        {
            _service = carGoService;
        }

        [HttpGet("vehicleStatus")]
        public async Task<IActionResult> GetAllAsync()
        {
            var vehicleStatus = await _service.GetAllAsync();

            if (vehicleStatus == null)
                return BadRequest();

            return Ok(vehicleStatus);
        }

        [HttpGet("vehicleStatusId")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var vehicleStatusId = await _service.GetByIdAsync(id);

            if (vehicleStatusId == null)
                return BadRequest();

            return Ok(vehicleStatusId);
        }
    }
}