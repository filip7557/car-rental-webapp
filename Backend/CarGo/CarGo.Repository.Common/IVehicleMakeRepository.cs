using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface IVehicleMakeRepository
    {
        public Task<List<VehicleMake>> GetAllAsync();

        public Task<VehicleMake?> GetByIdAsync(Guid id);
    }
}