﻿using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface ICompanyRequestService
    {
        Task<bool> NewCompanyRequest(CompanyRequest newCompanyRequest);

        Task<bool> CreateCompanyAsync(Company company);

        Task<bool> UpdateCompanyRequestAsync(CompanyRequest companyRequest);

        Task<bool> ManageCompanyRequest(Guid userId, bool isAccepted);

        Task<List<CompanyRequest>> GetCompanyRequestAsync();
    }
}