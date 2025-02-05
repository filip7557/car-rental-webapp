using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface ICompanyRequestRepository
    {
        public Task<bool> NewCompanyRequest(CompanyRequest newCompanyRequest);

        Task<bool> UpdateCompanyRequestAsync(CompanyRequest acceptedCompanyRequest);

        Task<CompanyRequest?> GetCompanyRequestByIdAsync(Guid id);
    }
}