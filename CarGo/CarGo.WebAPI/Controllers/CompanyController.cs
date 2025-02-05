using Autofac.Core;
using CarGo.Service.Common;
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

        [HttpPost]
        [HttpGet]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var company = await _companyService.GetCompanyAsync(id);
            if (company != null)
            {
                return Ok(company);
            }

            return NotFound("Company not found.");
        }
    }
}