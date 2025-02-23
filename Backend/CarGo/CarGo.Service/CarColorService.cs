using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace Service
{
    public class CarColorService : ICarColorService
    {
        private ICarColorRepository _carColorRepository;

        public CarColorService(ICarColorRepository repository)
        {
            _carColorRepository = repository;
        }

        public async Task<List<CarColor>?> GetAllAsync()
        {
            return await _carColorRepository.GetAllAsync();
        }

        public async Task<CarColor?> GetByIdAsync(Guid id)
        {
            return await _carColorRepository.GetByIdAsync(id);
        }
    }
}