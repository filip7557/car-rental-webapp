using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace CarGo.Service
{
    public class CompanyRequestService : ICompanyRequestService
    {
        private readonly ICompanyRequestRepository _repository;

        public CompanyRequestService(ICompanyRequestRepository companyRequestRepository)
        {
            _repository = companyRequestRepository;
        }

        public async Task<bool> NewCompanyRequest(CompanyRequest newCompanyRequest)
        {
            return await _repository.NewCompanyRequest(newCompanyRequest);
        }

        public async Task<bool> AcceptCompanyRequest(CompanyRequest acceptedCompanyRequest)
        {
            return await _repository.AcceptCompanyRequest(acceptedCompanyRequest);
        }
    }
}