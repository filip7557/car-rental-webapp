using CarGo.Common;
using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface INotificationRepository
    {
        public Task<bool> SaveNotificationAsync(Notification notification);
        public Task<List<Notification>> GetAllNotificationsAsync(Paging paging);
        public Task<int> CountAsync();
    }
}
