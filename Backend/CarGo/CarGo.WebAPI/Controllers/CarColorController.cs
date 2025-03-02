using CarGo.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarGoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarColorController : ControllerBase
    {
        private ICarColorService _service;

        public CarColorController(ICarColorService carGoService)
        {
            _service = carGoService;
        }

        [Authorize(Roles = "User,Manager,Administrator")]
        [HttpGet("carColors")]
        public async Task<IActionResult> GetAllAsync()
        {
            var colors = await _service.GetAllAsync();

            return Ok(colors);
        }

        [Authorize(Roles = "User,Manager,Administrator")]
        [HttpGet("{carColorId}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var carColorsId = await _service.GetByIdAsync(id);

            if (carColorsId == null)
                return BadRequest();

            return Ok(carColorsId);
        }
    }
}