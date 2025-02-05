using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface IVehicleTypeRepository
    {
        public Task<List<VehicleType>> GetAllAsync();
        public Task<VehicleType?> GetByIdAsync(Guid id);
    }
}