using CarGo.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [Authorize(Roles = "User,Manager,Administrator")]
        [HttpGet("vehicleStatus")]
        public async Task<IActionResult> GetAllAsync()
        {
            var vehicleStatus = await _service.GetAllAsync();

            if (vehicleStatus == null)
                return BadRequest();

            return Ok(vehicleStatus);
        }

        [Authorize(Roles = "User,Manager,Administrator")]
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