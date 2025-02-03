using CarGo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Service.Common
{
    public interface IBookService
    {
        Task<List<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(Guid id);
        Task AddBookingAsync(Booking booking);
        Task UpdateBookingAsync(Guid id, Booking updatedBooking);
        Task DeleteBookingAsync(Guid id);
    }
}
