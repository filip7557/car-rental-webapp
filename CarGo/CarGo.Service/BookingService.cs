using CarGo.Common;
using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace CarGo.Service
{
    public class BookingService : IBookService
    {
        private IBookRepository _repository;
        private readonly ITokenService _tokenService;
        

        public BookingService(IBookRepository repository, ITokenService tokenService, IManagerRepository managerRepository)
        {
            _repository = repository;
            _tokenService = tokenService;
   
        }

        public async Task<List<Booking>> GetAllBookingsAsync(BookingSorting sorting, BookingPaging paging,
            BookingFilter filter)
        {
            var userId = _tokenService.GetCurrentUserId();
            var userRole=_tokenService.GetCurrentUserRoleName();
            return await _repository.GetAllBookingsAsync(sorting, paging, filter, userId, userRole);
        }

        public async Task<Booking> GetBookingByIdAsync(Guid id)
        {
            return await _repository.GetBookingByIdAsync(id);
        }

        public async Task AddBookingAsync(Booking booking)
        {
            var userId = _tokenService.GetCurrentUserId();
            await _repository.AddBookingAsync(booking, userId);
        }

        public async Task UpdateBookingAsync(Guid id, Booking updatedBooking)
        {
            var userId = _tokenService.GetCurrentUserId();
            await _repository.UpdateBookingAsync(id, updatedBooking, userId);
        }

        public async Task UpdateBookingStatusAsync(Guid id, BookingStatus status)
        {
            var userId = _tokenService.GetCurrentUserId();
            await _repository.UpdateBookingStatusAsync(id, status, userId);
        }

        public async Task SoftDeleteBookingAsync(Guid id)
        {
            await _repository.SoftDeleteBookingAsync(id);
        }
    }
}