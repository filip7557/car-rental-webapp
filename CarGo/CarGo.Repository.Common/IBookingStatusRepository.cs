using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface IBookingStatusRepository
    {
        public Task<List<BookingStatus>> GetAllAsync();
        public Task<BookingStatus?> GetByIdAsync(Guid id);
    }
}