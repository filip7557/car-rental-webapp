using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface IVehicleMakeService
    {
        public Task<List<VehicleMake>?> GetAllAsync();
        public Task<VehicleMake?> GetByIdAsync(Guid id);
    }
}