using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface IVehicleStatusRepository
    {
        public Task<List<VehicleStatus>> GetAllAsync();

        public Task<VehicleStatus?> GetByIdAsync(Guid id);
    }
}