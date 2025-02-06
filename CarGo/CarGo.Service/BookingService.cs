using CarGo.Common;
using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace CarGo.Service
{
    public class BookingService : IBookService
    {
        private IBookRepository _repository;

        public BookingService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Booking>> GetAllBookingsAsync(BookingSorting sorting, BookingPaging paging,
            BookingFilter filter)
        {
            return await _repository.GetAllBookingsAsync(sorting, paging, filter);
        }

        public async Task<Booking> GetBookingByIdAsync(Guid id)
        {
            return await _repository.GetBookingByIdAsync(id);
        }

        public async Task AddBookingAsync(Booking booking)
        {
            await _repository.AddBookingAsync(booking);
        }

        public async Task UpdateBookingAsync(Guid id, Booking updatedBooking)
        {
            await _repository.UpdateBookingAsync(id, updatedBooking);
        }

        public async Task SoftDeleteBookingAsync(Guid id)
        {
            await _repository.SoftDeleteBookingAsync(id);
        }
    }
}