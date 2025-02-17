using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace Service
{
    public class BookingStatusService : IBookingStatusService
    {
        private IBookingStatusRepository _bookingStatusRepository;

        public BookingStatusService(IBookingStatusRepository repository)
        {
            _bookingStatusRepository = repository;
        }

        public async Task<List<BookingStatus>?> GetAllAsync()
        {
            return await _bookingStatusRepository.GetAllAsync();
        }

        public async Task<BookingStatus?> GetByIdAsync(Guid id)
        {
            return await _bookingStatusRepository.GetByIdAsync(id);
        }
    }
}