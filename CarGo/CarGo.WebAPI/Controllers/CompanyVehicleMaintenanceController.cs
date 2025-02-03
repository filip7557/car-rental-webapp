using CarGo.Model;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace CarGo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyVehicleMaintenanceController : ControllerBase
    {
        private readonly ICompanyVehicleMaintenanceService _companyVehicleMaintenanceService;
        public CompanyVehicleMaintenanceController(ICompanyVehicleMaintenanceService companyVehicleMaintenanceService)
        {
            _companyVehicleMaintenanceService = companyVehicleMaintenanceService;
        }
        [HttpPost]
        public async Task<IActionResult> Save(CompanyVehicleMaintenance maintenance)
        {
            // TODO: Get currently logged in user id.
            var success = await _companyVehicleMaintenanceService.SaveCompanyVehicleMaintenance(maintenance, Guid.Empty);
            if (success)
            {
                return Ok("Saved.");
            }
            return BadRequest("Invalid data.");
        }
    }
}
