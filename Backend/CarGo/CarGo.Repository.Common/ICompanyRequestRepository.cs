﻿using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface ICompanyRequestRepository
    {
        public Task<bool> NewCompanyRequest(CompanyRequest newCompanyRequest);

        Task<bool> UpdateCompanyRequestAsync(CompanyRequest acceptedCompanyRequest);

        Task<CompanyRequest?> GetCompanyRequestByIdAsync(Guid id);

        Task<List<CompanyRequest>> GetCompanyRequestsAsync();

        public Task<bool> CreateCompanyAsync(Company company);
    }
}