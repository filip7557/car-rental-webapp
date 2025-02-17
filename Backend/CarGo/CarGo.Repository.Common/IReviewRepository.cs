using CarGo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Repository.Common
{
    public interface IReviewRepository
    {

        Task<List<Review>> GetReviewsByCompanyIdAsync(Guid companyId, bool isAdmin);

        Task<bool> AddReviewAsync(Review review, Guid userId);

        Task<bool> SoftDeleteReviewAsync(Guid id);

    }
}
