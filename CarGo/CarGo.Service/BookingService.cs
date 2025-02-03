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
    public class BookingService : IBookService
    {
        private IBookRepository _repository;

        public BookingService(IBookRepository repository) {
            _repository = repository;
        }


        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _repository.GetAllBookingsAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(Guid id)
        {
            return await _repository.GetBookingByIdAsync(id);
        }
        public Task AddBookingAsync(Booking booking)
        {
            return _repository.AddBookingAsync(booking);
        }

        public Task UpdateBookingAsync(Guid id, Booking updatedBooking)
        {
            return _repository.UpdateBookingAsync(id, updatedBooking);
        }

        public async Task DeleteBookingAsync(Guid id)
        {
             await _repository.DeleteBookingAsync(id);
        }


    }
}
