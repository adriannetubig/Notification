using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRFunction;
using SignalRModel;
using SignalRModel.Filter;

namespace SignalRWeb.Controllers.V2
{
    public class NotificationsController : BaseControllerV2
    {
        private readonly IFNotification _iFNotification;
        public NotificationsController(IFNotification iFNotification)
        {
            _iFNotification = iFNotification;
        }

        [HttpPut("SendMessageToAuthenticatedConsumer")]
        public async Task<IActionResult> WithAuthorization(Notification notification, CancellationToken cancellationToken)
        {
            notification.Sender = UserName;
            await _iFNotification.SendMessageToAuthenticatedConsumer(notification, cancellationToken, UserId);
            return Ok(User.Identities.FirstOrDefault().Name);
        }

        [AllowAnonymous, HttpPut("SendMessageToUnauthenticatedConsumer")]
        public async Task<IActionResult> WithoutAuthorization(Notification notification, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(notification.Sender))
                notification.Sender = "Unauthenticated";

            await _iFNotification.SendMessageToUnauthenticatedConsumer(notification, cancellationToken, UserId);
            return Ok(User.Identities.FirstOrDefault().Name);
        }

        [HttpDelete("{notificationId}")]
        public async Task<IActionResult> Delete(int notificationId, CancellationToken cancellationToken)
        {
            return Ok(await _iFNotification.Delete(notificationId, cancellationToken));
        }

        [HttpPost("")]
        public async Task<IActionResult> Update(Notification notification, CancellationToken cancellationToken)
        {
            return Ok(await _iFNotification.Update(notification, cancellationToken, UserId));
        }

        [HttpGet("")]
        public async Task<IActionResult> Read(NotificationFilter notificationFilter, CancellationToken cancellationToken)
        {
            return Ok(await _iFNotification.Read(notificationFilter ,cancellationToken));//Todo: Add Filter
        }
    }
}
