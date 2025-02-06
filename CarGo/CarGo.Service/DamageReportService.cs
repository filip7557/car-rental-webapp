﻿using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

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

        public async Task<bool> CreateDamageReportAsync(DamageReport damageReport)
        {
            if (damageReport == null)
                return false;

            var userId = _tokenService.GetCurrentUserId();
            damageReport.Id = Guid.NewGuid();

            return await _damageReportRepository.CreateDamageReportAsync(damageReport, userId);
        }

        public async Task<DamageReportDTO?> GetDamageReportByCompanyVehicleIdAsync(Guid companyVehicleId)
        {
            var damageReport = await _damageReportRepository.GetDamageReportByCompanyVehicleIdAsync(companyVehicleId);
            if (damageReport == null)
                return null;

            var damageReportDTO = new DamageReportDTO
            {
                Id = damageReport.Id,
                Title = damageReport.Title,
                Description = damageReport.Description,
            };

            return damageReportDTO;
        }
    }
}
