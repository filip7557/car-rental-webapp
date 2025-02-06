using CarGo.Common;
using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace CarGo.Service
{
    public class CompanyVehicleService : ICompanyVehicleService
    {
        private ICompanyVehicleRepository _repository;
        private readonly ITokenService _tokenService;

        public CompanyVehicleService(ICompanyVehicleRepository repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public async Task<List<CompanyVehicle>> GetAllCompanyVehiclesAsync(BookingSorting sorting, Paging paging,
            CompanyVehicleFilter filter)
        {
            return await _repository.GetAllCompanyVehiclesAsync(sorting, paging, filter);
        }

        public async Task<CompanyVehicle> GetCompanyVehicleByIdAsync(Guid id)
        {
            return await _repository.GetCompanyVehicleByIdAsync(id);
        }

        public async Task AddCompanyVehicleAsync(CompanyVehicle companyVehicle)
        {
            var userId = _tokenService.GetCurrentUserId();
            await _repository.AddCompanyVehicleAsync(companyVehicle, userId);
        }

        public async Task UpdateCompanyVehicleAsync(Guid id, CompanyVehicle updatedCompanyVehicle)
        {
            var userId = _tokenService.GetCurrentUserId();
            await _repository.UpdateCompanyVehicleAsync(id, updatedCompanyVehicle, userId);
        }

        public async Task DeleteCompanyVehicleAsync(Guid id)
        {
            // TODO: Dodaj updatedByUserId i soft delete
            var userId = _tokenService.GetCurrentUserId();
            await _repository.DeleteCompanyVehicleAsync(id);
        }
    }
}