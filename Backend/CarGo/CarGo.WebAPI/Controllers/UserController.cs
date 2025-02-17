using CarGo.Model;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarGo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService service)
        {
            _userService = service;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _userService.GetUserDTOByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UserDTO user)
        {
            var success = await _userService.UpdateUserByIdAsync(id, user);
            if (!success)
            {
                return NotFound();
            }

            return Ok("Updated.");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("updateRole/{id}")]
        public async Task<IActionResult> UpdateRoleByUserIdAsync(Guid id, User user)
        {
            if (user == null || user.RoleId == null)
                return BadRequest();

            var result = await _userService.UpdateUserRoleByUserIdAsync(id, (Guid)user.RoleId);

            if (result)
                return Ok("Updated.");

            return BadRequest();
        }
    }
}