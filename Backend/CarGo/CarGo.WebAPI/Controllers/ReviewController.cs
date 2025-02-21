using Autofac.Core;
using CarGo.Model;
using CarGo.Service;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarGo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

       
        public ReviewController(IReviewService service)
        {
            _reviewService = service;
        }

       
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetReviewsByCompanyIdAsync(Guid id) 
        {
            try
            {
                var reviews = await _reviewService.GetReviewsByCompanyIdAsync(id);
                return reviews.Count > 0 ? Ok(reviews) : NotFound("There are no reviews");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error when fetching reviews: {ex.Message}");
            }
        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddReviewAsync(Review review )
        {
            if (review == null)
                return BadRequest();

            var success = await _reviewService.AddReviewAsync(review);

            if (success)
                return Ok("Created.");
            return BadRequest();
        }


        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDamageReportAsync(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var success = await _reviewService.SoftDeleteReviewAsync(id);

            if (success)
                return Ok("Deleted.");
            return BadRequest();
        }


    }
}
