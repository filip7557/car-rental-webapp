using Microsoft.AspNetCore.Mvc;
using CarGo.Service.Common;

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

        [HttpGet("carColors")]
        public async Task<IActionResult> GetAllAsync()
        {
            var colors = await _service.GetAllAsync();

            if (colors == null)
                return BadRequest();

            return Ok(colors);
        }

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