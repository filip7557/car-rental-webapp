using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarGo.Common;
using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface ICompanyVehicleService
    {
        Task<List<CompanyVehicle>> GetAllCompanyVehiclesAsync(BookingSorting sorting, Paging paging, CompanyVehicleFilter filter);

        Task<CompanyVehicle> GetCompanyVehicleByIdAsync(Guid id);

        Task AddCompanyVehicleAsync(CompanyVehicle companyVehicle, Guid userId);

        Task UpdateCompanyVehicleAsync(Guid id, CompanyVehicle updatedCompanyVehicle, Guid userId);

        Task DeleteCompanyVehicleAsync(Guid id);
    }
}