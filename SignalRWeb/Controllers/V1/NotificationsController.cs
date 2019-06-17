using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRFunction;
using SignalRModel;

namespace SignalRWeb.Controllers.V1
{
    public class NotificationsController : BaseControllerV1
    {
        private readonly IFNotification _iFNotification;
        public NotificationsController(IFNotification iFNotification)
        {
            _iFNotification = iFNotification;
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


        [AllowAnonymous, HttpGet]
        public async Task<IActionResult> Read(CancellationToken cancellationToken)
        {
            return Ok(await _iFNotification.Read(cancellationToken));//Todo: Add Filter
        }
    }
}
