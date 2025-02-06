using CarGo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Service.Common
{
    public interface IReviewService
    {
        Task<List<Review>> GetReviewsByCompanyIdAsync(Guid id);

        Task<bool> AddReviewAsync(Review review);

        Task<bool> SoftDeleteReviewAsync(Guid id);
    }
}
