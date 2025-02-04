using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface ICompanyRequestService
    {
        Task<bool> NewCompanyRequest(CompanyRequest newCompanyRequest);

        Task<bool> AcceptCompanyRequest(CompanyRequest acceptedCompanyRequest);
    }
}