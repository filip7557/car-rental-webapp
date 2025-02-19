using CarGo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Repository.Common
{
    public interface ICompanyLocationRepository
    {
        Task<bool> AddCompanyLocationAsync(Guid companyId, Guid locationId, Guid userId);
        Task<List<Location>> GetLocationByCompanyIdAsync(Guid companyId);


    }
}
