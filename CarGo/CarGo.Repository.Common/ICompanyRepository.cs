using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface ICompanyRepository
    {
        public Task<CompanyInfoDto?> GetCompanyAsync(Guid id);
    }
}