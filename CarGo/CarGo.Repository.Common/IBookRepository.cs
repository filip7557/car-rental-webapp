using CarGo.Common;
using CarGo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Repository.Common
{
    public interface IBookRepository
    {
        Task<List<Booking>> GetAllBookingsAsync(BookingSorting sorting, BookingPaging paging, BookingFilter filter);

        Task<Booking> GetBookingByIdAsync(Guid Id);

        Task DeleteBookingAsync(Guid Id);

        Task AddBookingAsync(Booking booking);

        Task UpdateBookingAsync(Guid id, Booking updatedBooking);
    }
}
