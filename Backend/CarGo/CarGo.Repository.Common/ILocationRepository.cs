using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface ILocationRepository
    {
        Task<List<Location>> GetLocationAsync();

        Task<Location> GetByIdLocationAsync(Guid id);

        Task<bool> PostLocationAsync(Location entity, Guid id);

        Task<bool> DeleteLocationAsync(Guid locationId, Guid id);

        //Task<bool> Delete(Guid id);
    }
}