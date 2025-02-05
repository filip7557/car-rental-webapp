using System.Security.Claims;
using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Components;
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
        private readonly ICompanyService _companyService;

        public CompanyRequestController(ICompanyRequestService companyRequestService, ICompanyRequestRepository companyRequestRepository, ICompanyService companyService)
        {
            _companyRequestService = companyRequestService;
            _companyRequestRepository = companyRequestRepository;
            _companyService = companyService;
        }

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

        [HttpPut("manage-company-request/{userId}")]
        public async Task<IActionResult> ManageCompanyRequest(Guid userId, [FromBody] bool isAccepted)
        {
            try
            {
                var companyRequest = await _companyRequestRepository.GetCompanyRequestByIdAsync(userId);

                if (companyRequest == null)
                {
                    return NotFound("Company request not found for the specified user.");
                }

                bool result = false;

                if (isAccepted)
                {
                    var newCompany = new Company
                    {
                        Id = Guid.NewGuid(),
                        Name = companyRequest.Name,
                        Email = companyRequest.Email,
                        IsActive = true,
                        CreatedByUserId = userId,
                        DateCreated = DateTime.UtcNow,
                        UpdatedByUserId = userId,
                    };

                    var resultOfCreatingCompany = await _companyService.CreateCompanyAsync(newCompany);

                    if (resultOfCreatingCompany)
                    {
                        companyRequest.IsApproved = true;
                        companyRequest.IsActive = false;
                        result = await _companyRequestRepository.UpdateCompanyRequestAsync(companyRequest);
                    }
                }
                else
                {
                    companyRequest.IsApproved = false;
                    companyRequest.IsActive = false;
                    result = await _companyRequestRepository.UpdateCompanyRequestAsync(companyRequest);
                }
                if (result)
                {
                    return Ok(isAccepted ? "Company request accepted successfully." : "Company request rejected.");
                }
                return BadRequest("Failed to update company request.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occurred: {e.Message}");
                return BadRequest("Failed to update company request.");
            }
        }
    }
}