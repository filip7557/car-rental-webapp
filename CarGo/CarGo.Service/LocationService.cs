using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace CarGo.Service
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository location)
        {
            _locationRepository = location;
        }

        public async Task<List<Location>> GetAsync()
        {
            return await _locationRepository.GetLocationAsync();
        }

        public async Task<Location> GetByIdAsync(Guid id)
        {
            return await _locationRepository.GetByIdLocationAsync(id);
        }

        public async Task<bool> PostAsync(Location entity, Guid id)
        {
            return await _locationRepository.PostLocationAsync(entity, id);
        }

        public async Task<bool> DeleteAsync(Guid locationId, Guid id)
        {
            
            return await _locationRepository.DeleteLocationAsync(locationId, id);
        }
    }
}