using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface IDamageReportService
    {
        Task<bool> CreateDamageReportAsync(DamageReport damageReport);

        Task<DamageReportDTO?> GetDamageReportByCompanyVehicleIdAsync(Guid companyVehicleId);
    }
}
