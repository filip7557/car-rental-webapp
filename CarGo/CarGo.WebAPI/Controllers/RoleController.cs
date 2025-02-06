using CarGo.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarGoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleService _service;

        public RoleController(IRoleService roleService)
        {
            _service = roleService;
        }

        [Authorize(Roles = "Manager,Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var roles = await _service.GetAllAsync();

            if (roles == null)
                return BadRequest();

            return Ok(roles);
        }

        [Authorize(Roles = "Manager,Administrator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var rolesId = await _service.GetByIdAsync(id);

            if (rolesId == null)
                return BadRequest();

            return Ok(rolesId);
        }
    }
}