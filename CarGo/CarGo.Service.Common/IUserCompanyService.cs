using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface IUserCompanyService
    {
        Task<bool> InsertUserCompanyAsync(UserCompany userCompany);
    }
}