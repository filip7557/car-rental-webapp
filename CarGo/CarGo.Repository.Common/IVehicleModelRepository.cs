using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface IVehicleModelRepository
    {
        public Task<List<VehicleModel>> GetAllAsync();

        public Task<VehicleModel?> GetByIdAsync(Guid id);
        Task<bool> AddAsync(VehicleModel vehicleModel, Guid userId); 
        Task<bool> DeleteAsync(Guid vehicleId, Guid id); 
    }
}