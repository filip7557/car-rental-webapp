using CarGo.Common;
using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface IBookService
    {
        Task<List<Booking>> GetAllBookingsAsync(BookingSorting sorting, BookingPaging paging, BookingFilter filter);

        Task<Booking> GetBookingByIdAsync(Guid id);

        Task AddBookingAsync(Booking booking);

        Task UpdateBookingAsync(Guid id, Booking updatedBooking);

        Task DeleteBookingAsync(Guid id);
    }
}