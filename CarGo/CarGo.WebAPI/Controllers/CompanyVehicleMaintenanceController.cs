using CarGo.Common;
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
        public async Task<IActionResult> SaveAsync(CompanyVehicleMaintenance maintenance)
        {
            // TODO: Get currently logged in user id.
            var success = await _companyVehicleMaintenanceService.SaveCompanyVehicleMaintenanceAsync(maintenance, Guid.Empty);
            if (success)
            {
                return Ok("Saved.");
            }
            return BadRequest("Invalid data.");
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(Guid id, int rpp = 10, int pageNumber = 1)
        {
            var paging = new Paging
            {
                Rpp = rpp,
                PageNumber = pageNumber
            };
            var response = await _companyVehicleMaintenanceService.GetMaintenancesByCompanyVehicleIdAsync(id, paging);
            if (response.Data.Count == 0)
                return BadRequest();
            return Ok(response);
        }
    }
}