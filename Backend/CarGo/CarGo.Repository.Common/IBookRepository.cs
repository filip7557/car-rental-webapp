using CarGo.Common;
using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface IBookRepository
    {
        Task<List<Booking>> GetAllBookingsAsync(BookingSorting sorting, BookingPaging paging, BookingFilter filter );

        Task<Booking> GetBookingByIdAsync(Guid Id);

        Task SoftDeleteBookingAsync(Guid Id);

        Task AddBookingAsync(Booking booking, Guid createdByUserId);

        Task UpdateBookingAsync(Guid id, Booking updatedBooking, Guid createdByUserId);
        Task UpdateBookingStatusAsync(Guid id, Guid statusId, Guid createdByUserId);
    }
}