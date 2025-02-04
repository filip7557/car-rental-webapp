﻿using CarGo.Common;
using CarGo.Model;

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