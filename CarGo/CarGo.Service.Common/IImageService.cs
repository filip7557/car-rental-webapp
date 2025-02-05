using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface IImageService
    {
        public Task<bool> SaveImageAsync(ImageDTO image, Guid createdByUserId);

        public Task<List<Guid>> GetImageIdsByDamageReportAsync(Guid damageReportId);

        public Task<Image?> GetImageByIdAsync(Guid imageId);

        public Task<bool> DeleteImageByIdAsync(Guid imageId);

        public Task<bool> SaveImagesAsync(List<ImageDTO> images, Guid userId);
    }
}