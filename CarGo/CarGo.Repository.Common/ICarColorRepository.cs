using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface ICarColorRepository
    {
        public Task<List<CarColor>> GetAllAsync();

        public Task<CarColor?> GetByIdAsync(Guid id);
    }
}