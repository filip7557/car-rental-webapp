using CarGo.Model;
using CarGo.Repository.Common;

namespace CarGo.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private string? _connectionString;

        public NotificationRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb")
            ?? throw new InvalidOperationException("Database connection string is not set.");
        }
        public async Task<bool> SaveNotificationAsync(Notification notification)
        {
            try
            {
                using (var connection )
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
                return false;
            }
        }
    }
}
