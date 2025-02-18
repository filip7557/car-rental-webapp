using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface IDamageReportService
    {
        Task<Guid> CreateDamageReportAsync(DamageReport damageReport);

        Task<List<DamageReportDTO>> GetDamageReportByCompanyVehicleIdAsync(Guid companyVehicleId);

        Task<bool> DeleteDamageReportAsync(Guid damageReportId);
    }
}