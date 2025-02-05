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
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;

        public CompanyService(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<CompanyInfoDto?> GetCompanyAsync(Guid id)
        {
            return await _repository.GetCompanyAsync(id);
        }

        public async Task<List<CompanyInfoIdAndNameDto>> GetCompaniesAsync()
        {
            return await _repository.GetCompaniesAsync();
        }

        public async Task<bool> CreateCompanyAsync(Company company)
        {
            return await _repository.CreateCompanyAsync(company);
        }
    }
}