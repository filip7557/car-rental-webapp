using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace CarGo.Service
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ITokenService _tokenService;

        public LocationService(ILocationRepository location, ITokenService tokenService)
        {
            _locationRepository = location;
            _tokenService = tokenService;
        }

        public async Task<List<Location>> GetAsync()
        {
            return await _locationRepository.GetLocationAsync();
        }

        public async Task<Location> GetByIdAsync(Guid id)
        {
            return await _locationRepository.GetByIdLocationAsync(id);
        }

        public async Task<bool> PostAsync(Location entity)
        {
            var createdByUserId = _tokenService.GetCurrentUserId();
            return await _locationRepository.PostLocationAsync(entity, createdByUserId);
        }

        public async Task<bool> DeleteAsync(Guid locationId)
        {
            var updatedByUserId = _tokenService.GetCurrentUserId();
            return await _locationRepository.DeleteLocationAsync(locationId, updatedByUserId);
        }
    }
}