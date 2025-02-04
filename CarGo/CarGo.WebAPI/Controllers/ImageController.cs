using CarGo.Model;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<IActionResult> SaveImageAsync(ImageDTO image)
        {
            if (image == null)
                return BadRequest();

            // TODO: Get current logged in user.

            var success = await _imageService.SaveImageAsync(image, Guid.Parse("a025b4b1-f76b-4770-87bb-a71266576201")); //test guid
            if (success)
                return Ok("Saved.");
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImageById(Guid id)
        {
            var image = await _imageService.GetImageByIdAsync(id);
            if (image == null)
                return NotFound();
            return File(image.ImageFile, "image/*");
        }

        [HttpGet]
        public async Task<IActionResult> GetImageByDamageReportIdAsync(Guid damageReportId)
        {
            if (damageReportId == Guid.Empty)
                return BadRequest();

            return Ok(await _imageService.GetImageIdsByDamageReportAsync(damageReportId));
        }

        [HttpDelete("{imageId}")]
        public async Task<IActionResult> DeleteImageById(Guid imageId)
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