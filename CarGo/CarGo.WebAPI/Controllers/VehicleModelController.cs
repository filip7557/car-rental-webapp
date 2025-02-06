using System.Security.Claims;
using CarGo.Model;
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
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var vehicleModel = await _service.GetAllAsync();

            if (vehicleModel == null)
                return BadRequest();

            return Ok(vehicleModel);
        }

        [Authorize(Roles = "User,Manager,Administrator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var vehicleModelId = await _service.GetByIdAsync(id);

            if (vehicleModelId == null)
                return BadRequest("Bad request, no found such like that");

            return Ok(vehicleModelId);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(VehicleModel vehicle)
        {
            
            if(vehicle.Id == Guid.Empty)
            {
                vehicle.Id = Guid.NewGuid();
            }
            try{
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                await _service.AddAsync(vehicle, userId);
                return Ok(vehicle); 
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var comVehId = await _service.GetByIdAsync(id);
            if(comVehId == null)
            {
                return BadRequest("Company Vehicle with inputed id doesnt exist");
            }
            try{
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                await _service.DeleteAsync(id, userId);
                return Ok("Company Vehicle isActive status changed.");
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }

        }

    }
}