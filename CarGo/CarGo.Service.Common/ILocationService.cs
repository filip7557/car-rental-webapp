using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface ILocationService
    {
        Task<List<Location>> GetAsync();

        Task<Location> GetByIdAsync(Guid id);

        Task<bool> PostAsync(Location entity, Guid id);

        //Task<bool> Put(Guid id);

        Task<bool> DeleteAsync(Guid id);
    }
}