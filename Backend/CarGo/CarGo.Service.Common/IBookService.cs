using CarGo.Common;
using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface IBookService
    {
        Task<List<BookingDto>> GetAllBookingsAsync(BookingSorting sorting, BookingPaging paging, BookingFilter filter);

        Task<Booking> GetBookingByIdAsync(Guid id);

        Task AddBookingAsync(Booking booking);

        Task UpdateBookingAsync(Guid id, Booking updatedBooking);
        Task UpdateBookingStatusAsync(Guid id);
        Task SoftDeleteBookingAsync(Guid id);
    }
}