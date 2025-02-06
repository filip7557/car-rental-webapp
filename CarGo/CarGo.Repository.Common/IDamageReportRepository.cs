using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface IDamageReportRepository
    {
        Task<bool> CreateDamageReportAsync(DamageReport damageReport, Guid createdByUserId);
    }
}
