using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface ILocationService
    {
        Task<List<Location>> GetAsync();

        Task<List<Location>> GetLocationsByCompanyIdAsync(Guid companyId);

        Task<Location> GetByIdAsync(Guid id);

        Task<bool> PostAsync(Guid companyId, Location entity);

        //Task<bool> Put(Guid id);

        Task<bool> DeleteAsync(Guid locationId);
    }
}