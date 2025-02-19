using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CarGo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyRequestController : ControllerBase
    {
        private readonly ICompanyRequestService _companyRequestService;
        private readonly ICompanyRequestRepository _companyRequestRepository;

        public CompanyRequestController(ICompanyRequestService companyRequestService,
            ICompanyRequestRepository companyRequestRepository)
        {
            _companyRequestService = companyRequestService;
            _companyRequestRepository = companyRequestRepository;
        }

        [Authorize]
        [HttpPost("new-company-request")]
        public async Task<IActionResult> NewCompanyRequest([FromBody] CompanyRequest newCompanyRequest)
        {
            var result = await _companyRequestService.NewCompanyRequest(newCompanyRequest);
            if (result)
            {
                return Ok("Company request created successfully.");
            }

            return BadRequest("Failed to create company request.");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("manage-company-request/{userId}")]
        public async Task<IActionResult> ManageCompanyRequest(Guid userId, [FromBody] bool isAccepted)
        {
            var result = await _companyRequestService.ManageCompanyRequest(userId, isAccepted);
            if (!result)
            {
                return BadRequest(new { message = "Failed to manage company request.", details = "Additional error details here" });
            }
            return Ok("Company request managed successfully.");
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("get-all-company-requests")]
        public async Task<IActionResult> GetAllCompanyRequestsAsync()
        {
            var companyes = await _companyRequestRepository.GetCompanyRequestsAsync();
            if (companyes.Count > 0)
            {
                return Ok(companyes);
            }

            return NotFound(new
            {
                error = "Not found",
                message = "No company requests found."
            });
        }
    }
}