using CarGo.Common;
using CarGo.Model;
using CarGo.Repository;
using CarGo.Repository.Common;
using CarGo.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Service
{
    public class ReviewService: IReviewService
    {

        private readonly IReviewRepository _repository;
        private readonly ITokenService _tokenService;
        public ReviewService(IReviewRepository reviewRepository, ITokenService tokenService)
        {
            _repository = reviewRepository;
            _tokenService = tokenService;
        }


        public async Task<List<Review>> GetReviewsByCompanyIdAsync(Guid id)
        {
            var isAdmin = false;
            var userRole = _tokenService.GetCurrentUserRoleName();
          


            if (userRole== "Administrator")
            {
                 isAdmin = true;
            }
           
            return await _repository.GetReviewsByCompanyIdAsync(id,isAdmin);
        }

        public async Task<bool> AddReviewAsync(Review review)
        {
            try
            {
                var userId = _tokenService.GetCurrentUserId();

                review.Id = Guid.NewGuid();  

               
                await _repository.AddReviewAsync(review, userId);

                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
        }

        public async Task<bool> SoftDeleteReviewAsync(Guid id)
        {
            if (id == Guid.Empty)
                return false;

            return await _repository.SoftDeleteReviewAsync(id);
        }


    
    }

 

}
