using System.Security.Claims;
using CarGo.Model;
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

        public CompanyRequestController(ICompanyRequestService companyRequestService)
        {
            _companyRequestService = companyRequestService;
        }

        [HttpPost]
        public async Task<IActionResult> NewCompanyRequest([FromBody] CompanyRequest newCompanyRequest)
        {
            // FOR TESTING If the user id is not set, generate a new one for the request
            if (newCompanyRequest.UserId == Guid.Empty)
            {
                newCompanyRequest.UserId = Guid.NewGuid();
            }

            //Pass user id from token
            /*
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("User is not authenticated");
            }

            Guid userGuid;
            if (!Guid.TryParse(userId, out userGuid))
            {
                return Unauthorized("Invalit user id");
            }

            newCompanyRequest.UserId = userGuid;
            */

            var result = await _companyRequestService.NewCompanyRequest(newCompanyRequest);
            if (result)
            {
                return Ok("Company request created successfully.");
            }
            return BadRequest("Failed to create company request.");
        }

        [HttpPost]
        public async Task<IActionResult> AcceptCompanyRequest([FromBody] CompanyRequest acceptedCompanyRequest)
        {
            var result = await _companyRequestService.AcceptCompanyRequest(acceptedCompanyRequest);
            if (result)
            {
                return Ok("Company request accepted successfully.");
            }
            return BadRequest("Failed to accept company request.");
        }
    }
}