using MediatR;
using SignalRModel;
using SignalRModel.Events;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRFunction
{
    public class FNotification : IFNotification
    {
        private readonly IMediator _mediator;
        public FNotification(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task SendMessageToAuthenticatedConsumer(Notification notification, CancellationToken cancellationToken)
        {
            var authenticatedHubEvent = new AuthenticatedHubEvent
            {
                Sender = notification.Sender,
                Message = notification.Message
            };
            await _mediator.Publish(authenticatedHubEvent, cancellationToken);
        }
    }
}
