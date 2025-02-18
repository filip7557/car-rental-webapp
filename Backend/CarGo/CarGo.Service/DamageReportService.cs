using CarGo.Common;
using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;
using Microsoft.IdentityModel.Tokens;

namespace CarGo.Service
{
    public class DamageReportService : IDamageReportService
    {
        private readonly IDamageReportRepository _damageReportRepository;
        private readonly ITokenService _tokenService;

        public DamageReportService(IDamageReportRepository damageReportRepository, ITokenService tokenService)
        {
            _damageReportRepository = damageReportRepository;
            _tokenService = tokenService;
        }

        public async Task<Guid> CreateDamageReportAsync(DamageReport damageReport)
        {
            if (damageReport == null)
                return Guid.Empty;

            var userId = _tokenService.GetCurrentUserId();
            damageReport.Id = Guid.NewGuid();

            var result =  await _damageReportRepository.CreateDamageReportAsync(damageReport, userId);

            if (result)
                return damageReport.Id;
            else
                return Guid.Empty;
        }

        public async Task<List<DamageReportDTO>> GetDamageReportByCompanyVehicleIdAsync(Guid companyVehicleId)
        {
            var roleName = _tokenService.GetCurrentUserRoleName();
            var isAdmin = roleName.Equals(RoleName.Administrator);
            var damageReports = await _damageReportRepository.GetDamageReportByCompanyVehicleIdAsync(companyVehicleId, isAdmin);
            var damageReportDTOs = new List<DamageReportDTO>();
            if (damageReports.IsNullOrEmpty())
                return damageReportDTOs;

            foreach (var damageReport in damageReports)
            {
                var damageReportDTO = new DamageReportDTO
                {
                    Id = damageReport.Id,
                    Title = damageReport.Title,
                    Description = damageReport.Description,
                };
                damageReportDTOs.Add(damageReportDTO);
            }

            return damageReportDTOs;
        }

        public async Task<bool> DeleteDamageReportAsync(Guid damageReportId)
        {
            if (damageReportId == Guid.Empty)
                return false;

            return await _damageReportRepository.DeleteDamageReportAsync(damageReportId);
        }
    }
}
