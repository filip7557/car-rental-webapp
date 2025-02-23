using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface IBookingStatusService
    {
        public Task<List<BookingStatus>?> GetAllAsync();

        public Task<BookingStatus?> GetByIdAsync(Guid id);
    }
}