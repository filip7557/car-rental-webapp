using CarGo.Model;

namespace CarGo.Repository
{
    public interface IImageRepository
    {
        public Task<bool> SaveImageAsync(Image image, Guid createdByUserId);

        public Task<List<Guid>> GetImageIdsByDamageReportIdAsync(Guid damageReportId);
        public Task<Image?> GetImageById(Guid id);
    }
}