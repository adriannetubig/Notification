using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRFunction;
using SignalRModel;
using SignalRWeb.Hubs;

namespace SignalRWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IFNotification _iFNotification;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationsController(IFNotification iFNotification, IHubContext<NotificationHub> hubContext)
        {
            _iFNotification = iFNotification;
            _hubContext = hubContext;
        }


        [AllowAnonymous, HttpPost("NoAuthorization")]
        public IActionResult NoAuthorization()
        {
            Task.WaitAll(
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "Api", "NoAuthorization")
            );
            return Ok("NoAuthorization");
        }

        [Authorize, HttpPost("WithAuthorization")]
        public IActionResult WithAuthorization()
        {
            Task.WaitAll(
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "Api", User.Identities.FirstOrDefault().Name)
            );
            return Ok(User.Identities.FirstOrDefault().Name);
        }

        [Authorize, HttpPost("SendMessageToAuthenticatedConsumer")]
        public async Task<IActionResult> WithAuthorization(Notification notification, CancellationToken cancellationToken)
        {
            notification.Sender = User.Identities.FirstOrDefault().Name;
            await _iFNotification.SendMessageToAuthenticatedConsumer(notification, cancellationToken);
            return Ok(User.Identities.FirstOrDefault().Name);
        }

        [HttpPost("SendMessageToUnauthenticatedConsumer")]
        public async Task<IActionResult> WithoutAuthorization(Notification notification, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(notification.Sender))
                notification.Sender = "Unauthenticated";
            await _iFNotification.SendMessageToUnauthenticatedConsumer(notification, cancellationToken);
            return Ok(User.Identities.FirstOrDefault().Name);
        }
    }
}
