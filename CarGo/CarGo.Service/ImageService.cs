using CarGo.Model;
using CarGo.Repository;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Http;

namespace CarGo.Service
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly ITokenService _tokenService;

        public ImageService(IImageRepository imageRepository, ITokenService tokenService)
        {
            _imageRepository = imageRepository;
            _tokenService = tokenService;
        }

        public async Task<bool> SaveImageAsync(ImageDTO imageDto)
        {
            var userId = _tokenService.GetCurrentUserId();
            var image = new Image
            {
                ImageFile = await ConvertToByteArrayAsync(imageDto.ImageFile),
                DamageReportId = imageDto.DamageReportId,
            };
            return await _imageRepository.SaveImageAsync(image, userId);
        }

        public async Task<bool> SaveImagesAsync(List<ImageDTO> images)
        {
            var results = new List<bool>();
            foreach (var image in images)
            {
                results.Add(await SaveImageAsync(image));
            }

            return !results.Any(p => p == false);
        }

        public async Task<List<Guid>> GetImageIdsByDamageReportAsync(Guid damageReportId)
        {
            return await _imageRepository.GetImageIdsByDamageReportIdAsync(damageReportId);
        }

        public async Task<Image?> GetImageByIdAsync(Guid id)
        {
            return await _imageRepository.GetImageByIdAsync(id);
        }

        public async Task<bool> DeleteImageByIdAsync(Guid imageId)
        {
            return await _imageRepository.DeleteImageByIdAsync(imageId);
        }

        private async Task<byte[]> ConvertToByteArrayAsync(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}