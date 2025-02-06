using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface IVehicleModelService
    {
        public Task<List<VehicleModel>?> GetAllAsync();

        public Task<VehicleModel?> GetByIdAsync(Guid id);

        Task AddAsync(VehicleModel vehicleModel, Guid userId);

        Task DeleteAsync(Guid id);
    }
}