using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface IVehicleTypeService
    {
        public Task<List<VehicleType>?> GetAllAsync();

        public Task<VehicleType?> GetByIdAsync(Guid id);
    }
}