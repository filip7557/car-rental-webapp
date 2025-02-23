using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace CarGo.Service
{
    public class ReviewService: IReviewService
    {

        private readonly IReviewRepository _repository;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        public ReviewService(IReviewRepository reviewRepository, ITokenService tokenService, IUserService userService)
        {
            _repository = reviewRepository;
            _tokenService = tokenService;
            _userService = userService;
        }


        public async Task<List<ReviewDTO>> GetReviewsByCompanyIdAsync(Guid id)
        {
            var isAdmin = false;
            var userRole = _tokenService.GetCurrentUserRoleName();
          


            if (userRole== "Administrator")
            {
                 isAdmin = true;
            }
           
            var reviews =  await _repository.GetReviewsByCompanyIdAsync(id,isAdmin);
            var reviewDTOs = new List<ReviewDTO>();
            foreach (var review in reviews)
            {
                var user = await _userService.GetUserByIdAsync(review.CreatedByUserId);
                var reviewDTO = new ReviewDTO
                {
                    Title = review.Title,
                    Description = review.Description,
                    DateCreated = review.DateCreated,
                    User = user!.FullName
                };
                reviewDTOs.Add(reviewDTO);
            }
            return reviewDTOs;
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
