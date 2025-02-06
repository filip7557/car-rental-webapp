using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface IVehicleModelRepository
    {
        public Task<List<VehicleModel>> GetAllAsync();

        public Task<VehicleModel?> GetByIdAsync(Guid id);
        Task AddAsync(VehicleModel vehicleModel, Guid userId);
        Task DeleteAsync(Guid id);
    }
}