using CarGo.Common;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarGo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetAllNotificationsAsync(int rpp = 10, int pageNumber = 1)
        {
            var paging = new Paging
            {
                Rpp = rpp,
                PageNumber = pageNumber
            };

            var result = await _notificationService.GetAllNotificationsAsync(paging);

            return Ok(result);
        }
    }
}
