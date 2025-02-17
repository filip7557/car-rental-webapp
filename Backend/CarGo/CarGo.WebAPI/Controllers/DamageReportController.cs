using CarGo.Model;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarGo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DamageReportController : ControllerBase
    {
        private readonly IDamageReportService _damageReportService;

        public DamageReportController(IDamageReportService damageReportService)
        {
            _damageReportService = damageReportService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateDamageReportAsync(DamageReport damageReport)
        {
            if (damageReport == null)
                return BadRequest();

            var success = await _damageReportService.CreateDamageReportAsync(damageReport);

            if (success)
                return Ok("Created.");
            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetDamageReportByCompanyVehicleIdAsync(Guid companyVehicleId)
        {
            if (companyVehicleId == Guid.Empty)
                return BadRequest();

            var damageReportDTO = await _damageReportService.GetDamageReportByCompanyVehicleIdAsync(companyVehicleId);

            if (damageReportDTO == null)
                return NotFound();

            return Ok(damageReportDTO);
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpDelete("{damageReportId}")]
        public async Task<IActionResult> DeleteDamageReportAsync(Guid damageReportId)
        {
            if (damageReportId == Guid.Empty)
                return BadRequest();

            var success = await _damageReportService.DeleteDamageReportAsync(damageReportId);

            if (success)
                return Ok("Deleted.");
            return BadRequest();
        }
    }
}