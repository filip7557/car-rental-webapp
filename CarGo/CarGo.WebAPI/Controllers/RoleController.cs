using Microsoft.AspNetCore.Mvc;
using CarGo.Service.Common;

namespace CarGoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarGoRoleController : ControllerBase
    {
        private IRoleService _service;

        public CarGoRoleController(IRoleService carGoService)
        {
            _service = carGoService;
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetAllAsync()
        {
            var roles = await _service.GetAllAsync();

            if (roles == null)
                return BadRequest();

            return Ok(roles);
        }

        [HttpGet("{roles-id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var rolesId = await _service.GetByIdAsync(id);

            if (rolesId == null)
                return BadRequest();

            return Ok(rolesId);
        }
    }
}