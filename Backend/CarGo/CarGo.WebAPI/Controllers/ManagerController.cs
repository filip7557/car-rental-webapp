using CarGo.Model;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarGo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;
        private readonly IUserService _userService;

        public ManagerController(IManagerService managerService, IUserService userService)
        {
            _managerService = managerService;
            _userService = userService;
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetManagerByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var manager = await _managerService.GetManagerByIdAsync(id);

            if (manager == null)
                return NotFound();

            return Ok(manager);
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpGet("company")]
        public async Task<IActionResult> GetAllCompanyManagersAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
                return BadRequest();

            var managers = await _managerService.GetAllCompanyManagersAsync(companyId);

            if (!managers.Any())
                return NotFound();

            return Ok(managers);
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpPost("{companyId}")]
        public async Task<IActionResult> AddCompanyManagerAsync(Guid companyId, User user)
        {
            if (companyId == Guid.Empty)
                return BadRequest();

            if (user == null)
                return BadRequest();

            var result = await _managerService.AddManagerToCompanyAsync(companyId, user);

            if (result)
                return Ok("Added.");

            return BadRequest();
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpDelete("{companyId}")]
        public async Task<IActionResult> RemoveManagerFromCompanyAsync(Guid companyId, Guid userid)
        {
            if (companyId == Guid.Empty)
                return BadRequest();

            var user = await _userService.GetUserByIdAsync(userid);

            if (user == null)
                return BadRequest();

            var result = await _managerService.RemoveManagerFromCompanyAsync(companyId, user);

            if (result)
                return Ok("Removed.");

            return BadRequest();
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<Guid> GetCompanyIdByUserIdAsync(Guid userId)
        {
            return await _managerService.GetCompanyIdByUserIdAsync(userId);
        }
    }
}