using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarGo.Common;
using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface ICompanyVehicleRepository
    {
        Task<List<CompanyVehicle>> GetAllCompanyVehiclesAsync(BookingSorting sorting, Paging paging, CompanyVehicleFilter filter);

        Task<CompanyVehicle> GetCompanyVehicleByIdAsync(Guid id);

        Task AddCompanyVehicleAsync(CompanyVehicle companyVehicle, Guid userId);

        Task UpdateCompanyVehicleAsync(Guid id, CompanyVehicle updatedCompanyVehicle, Guid userId);

        Task<bool> DeleteCompanyVehicleAsync(Guid compVehId, Guid id);
    }
}
