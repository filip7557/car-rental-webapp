using CarGo.Common;
using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface INotificationService
    {
        public Task<bool> SendNotificationAsync(Notification notification);

        public Task<PagedResponse<Notification>> GetAllNotificationsAsync(Paging paging);
    }
}