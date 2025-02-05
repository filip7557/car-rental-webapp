﻿using CarGo.Model;
using CarGo.Repository;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Http;

namespace CarGo.Service
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<bool> SaveImageAsync(ImageDTO imageDto, Guid createdByUserId)
        {
            var image = new Image
            {
                ImageFile = await ConvertToByteArrayAsync(imageDto.ImageFile),
                DamageReportId = imageDto.DamageReportId,
            };
            return await _imageRepository.SaveImageAsync(image, createdByUserId);
        }

        public async Task<bool> SaveImagesAsync(List<ImageDTO> images, Guid createdByUserId)
        {
            var results = new List<bool>();
            foreach (var image in images)
            {
                results.Add(await SaveImageAsync(image, createdByUserId));
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