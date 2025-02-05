using CarGo.Model;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarGo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveImageAsync(ImageDTO image)
        {
            if (image == null)
                return BadRequest();

            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var success = await _imageService.SaveImageAsync(image, userId);
            if (success)
                return Ok("Saved.");
            return BadRequest();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveImagesAsync(List<ImageDTO> images)
        {
            if (!images.Any()) return BadRequest();
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var success = await _imageService.SaveImagesAsync(images, userId);
            if (success)
                return Ok("Saved.");
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImageByIdAsync(Guid id)
        {
            var image = await _imageService.GetImageByIdAsync(id);
            if (image == null)
                return NotFound();
            return File(image.ImageFile, "image/*");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetImageByDamageReportIdAsync(Guid damageReportId)
        {
            if (damageReportId == Guid.Empty)
                return BadRequest();

            return Ok(await _imageService.GetImageIdsByDamageReportAsync(damageReportId));
        }

        [Authorize]
        [HttpDelete("{imageId}")]
        public async Task<IActionResult> DeleteImageByIdAsync(Guid imageId)
        {
            if (imageId == Guid.Empty)
                return BadRequest();

            var success = await _imageService.DeleteImageByIdAsync(imageId);

            if (success)
                return Ok("Deleted");
            return BadRequest();
        }
    }
}