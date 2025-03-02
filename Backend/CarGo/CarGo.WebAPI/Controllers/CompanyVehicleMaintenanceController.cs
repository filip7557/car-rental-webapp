﻿using CarGo.Common;
using CarGo.Model;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize(Roles = "Administrator,Manager")]
        [HttpPost]
        public async Task<IActionResult> SaveAsync(CompanyVehicleMaintenance maintenance)
        {
            var success = await _companyVehicleMaintenanceService.SaveCompanyVehicleMaintenanceAsync(maintenance);
            if (success)
            {
                return Ok("Saved.");
            }

            return BadRequest("Invalid data.");
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var success = await _companyVehicleMaintenanceService.DeleteCompanyVehicleMaintenanceByIdAsync(id);
            if (success)
            {
                return Ok("Deleted.");
            }

            return BadRequest();
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpGet]
        public async Task<IActionResult> GetByCompanyVehicleIdAsync(Guid id, int rpp = 10, int pageNumber = 1)
        {
            var paging = new Paging
            {
                Rpp = rpp,
                PageNumber = pageNumber
            };
            var response = await _companyVehicleMaintenanceService.GetMaintenancesByCompanyVehicleIdAsync(id, paging);
            if (response == null)
                return BadRequest();
            if (response.Data.Count == 0)
                return BadRequest();
            return Ok(response);
        }
    }
}