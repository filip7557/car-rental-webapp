using CarGo.Model;
using CarGo.Repository;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace CarGo.Service
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ICompanyLocationRepository _companyLocationRepository;
        private readonly ITokenService _tokenService;

        public LocationService(ILocationRepository location, ITokenService tokenService, ICompanyLocationRepository companyLocation)
        {
            _companyLocationRepository = companyLocation;
            _locationRepository = location;
            _tokenService = tokenService;
        }

        public async Task<List<Location>> GetAsync()
        {
            return await _locationRepository.GetLocationAsync();
        }


        public async Task<List<Location>> GetLocationsByCompanyIdAsync(Guid companyId)
        {
            // Pozivamo metodu iz repozitorija koja dohvaća sve lokacije prema companyId
            return await _companyLocationRepository.GetLocationByCompanyIdAsync(companyId);
        }

        public async Task<Location> GetByIdAsync(Guid id)
        {
            return await _locationRepository.GetByIdLocationAsync(id);

        }
        public async Task<bool> PostAsync(Guid companyId, Location entity)
        {
          
            entity.Id = Guid.NewGuid();

          
            var createdByUserId = _tokenService.GetCurrentUserId();

            
            bool locationAdded = await _locationRepository.PostLocationAsync(entity, createdByUserId);
            if (locationAdded)
            {
                
                bool companyLocationAdded = await _companyLocationRepository.AddCompanyLocationAsync(companyId, entity.Id, createdByUserId);

               
                return companyLocationAdded;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid locationId)
        {
            var updatedByUserId = _tokenService.GetCurrentUserId();
            return await _locationRepository.DeleteLocationAsync(locationId, updatedByUserId);
        }
    }
}