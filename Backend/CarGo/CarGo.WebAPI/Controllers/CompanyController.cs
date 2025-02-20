using Autofac.Core;
using CarGo.Model;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarGo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpPost("new-company-location")]
        public async Task<IActionResult> NewCompanyLocation([FromBody] CompanyLocations companyLocations)
        {
            var result = await _companyService.NewCompanyLocationAsync(companyLocations);
            if (result)
            {
                return Ok("Company location created successfully.");
            }
            return BadRequest("Failed to create company location.");
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpDelete("delete-company-location")]
        public async Task<IActionResult> DeleteCompanyLocation([FromBody] CompanyLocations companyLocations)
        {
            var result = await _companyService.DeleteCompanyLocationAsync(companyLocations);
            if (result)
            {
                return Ok("Company location deleted successfully.");
            }
            return BadRequest("Failed to delete company location.");
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpPut("edit-company-location")]
        public async Task<IActionResult> UpdateCompanyLocation(Guid Id, [FromBody] CompanyLocations companyLocations)
        {
            var result = await _companyService.UpdateCompanyLocationAsync(Id, companyLocations);
            if (result)
            {
                return Ok("Company location updated successfully.");
            }
            return BadRequest("Failed to update company location.");
        }

        [Authorize]
        [HttpGet("get-all-companyes")]
        public async Task<IActionResult> GetCompanyesAsync()
        {
            var companyes = await _companyService.GetCompaniesAsync();

            if (companyes.Count > 0)
            {
                return Ok(companyes);
            }

            return NotFound(new
            {
                error = "Not found",
                message = "No companyes found."
            });
        }

        [Authorize]
        [HttpGet("get-company-info-by-id/{id}")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var company = await _companyService.GetCompanyAsync(id);
            if (company != null)
            {
                return Ok(company);
            }

            return NotFound("Company not found.");
        }

        [Authorize]
        [HttpGet("get-all-company-locations")]
        public async Task<IActionResult> GetAllCompanyLocationsAsync()
        {
            var companyLocations = await _companyService.GetAllCompanyLocationsAsync();
            if (companyLocations.Count > 0)
            {
                return Ok(companyLocations);
            }
            return NotFound("No company locations found.");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("create-company-by-admin")]
        public async Task<IActionResult> CreateCompanyByAdminAsync([FromBody] Company company)
        {
            var result = await _companyService.CreateCompanyByAdminAsync(company);
            if (result)
            {
                return Ok("Company created successfully.");
            }
            return BadRequest("Failed to create company.");
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpPut("delete-company,{id},{isActive}")]
        public async Task<IActionResult> ChangeCompanyIsActiveStatusAsync([FromRoute] Guid Id, [FromRoute] bool isActive)
        {
            var result = await _companyService.ChangeCompanyIsActiveStatusAsync(Id, isActive);
            if (result)
            {
                return Ok("Company active status changed successfully.");
            }
            return BadRequest("Failed to change company active status.");
        }
    }
}