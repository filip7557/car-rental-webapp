using CarGo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Repository.Common
{
    public interface INotificationRepository
    {
        public Task<bool> SaveNotificationAsync(Notification notification);
    }
}
