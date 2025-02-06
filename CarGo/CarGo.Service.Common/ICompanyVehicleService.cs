using System;
using System.Collections.Generic;
using System.Linq;
using CarGo.Common;
using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface ICompanyVehicleService
    {
        Task<List<CompanyVehicle>> GetAllCompanyVehiclesAsync(BookingSorting sorting, Paging paging,
            CompanyVehicleFilter filter);

        Task<CompanyVehicle> GetCompanyVehicleByIdAsync(Guid id);

        Task AddCompanyVehicleAsync(CompanyVehicle companyVehicle);

        Task UpdateCompanyVehicleAsync(Guid id, CompanyVehicle updatedCompanyVehicle);

        Task DeleteCompanyVehicleAsync(Guid id);
    }
}