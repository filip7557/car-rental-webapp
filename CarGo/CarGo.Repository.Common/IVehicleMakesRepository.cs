using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface IVehicleMakesRepository
    {
        public Task<List<VehicleMakes>> GetAllAsync();
        public Task<VehicleMakes?> GetByIdAsync(Guid id);
    }
}