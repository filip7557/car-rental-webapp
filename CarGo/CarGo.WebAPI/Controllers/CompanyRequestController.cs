using CarGo.Model;
using CarGo.Repository.Common;
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

                    var resultOfCreatingCompany = await _companyRequestService.CreateCompanyAsync(newCompany);

                    if (resultOfCreatingCompany)
                    {
                        companyRequest.IsApproved = true;
                        companyRequest.IsActive = false;
                        result = await _companyRequestService.UpdateCompanyRequestAsync(companyRequest);
                    }
                }
                else
                {
                    companyRequest.IsApproved = false;
                    companyRequest.IsActive = false;
                    result = await _companyRequestService.UpdateCompanyRequestAsync(companyRequest);
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