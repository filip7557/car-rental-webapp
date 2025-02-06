using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface IDamageReportRepository
    {
        Task<bool> CreateDamageReportAsync(DamageReport damageReport, Guid createdByUserId);

        Task<DamageReport?> GetDamageReportByCompanyVehicleIdAsync(Guid companyVehicleId, bool isAdmin);

        Task<bool> DeleteDamageReportAsync(Guid damageReportId);
    }
}
