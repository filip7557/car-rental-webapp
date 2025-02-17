using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface ICarColorService
    {
        public Task<List<CarColor>?> GetAllAsync();

        public Task<CarColor?> GetByIdAsync(Guid id);
    }
}