using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface IReviewService
    {
        Task<List<ReviewDTO>> GetReviewsByCompanyIdAsync(Guid id);

        Task<bool> AddReviewAsync(Review review);

        Task<bool> SoftDeleteReviewAsync(Guid id);
    }
}
