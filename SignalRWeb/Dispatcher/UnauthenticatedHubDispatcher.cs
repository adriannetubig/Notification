using MediatR;
using Microsoft.AspNetCore.SignalR;
using SignalRModel.Events;
using SignalRWeb.Hubs;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRWeb.Dispatcher
{
    public class UnauthenticatedHubDispatcher : INotificationHandler<UnauthenticatedHubEvent>
    {
        private readonly IHubContext<UnauthenticatedHub> _hubContext;
        public UnauthenticatedHubDispatcher(IHubContext<UnauthenticatedHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public Task Handle(UnauthenticatedHubEvent @event, CancellationToken cancellationToken)
        {
            return _hubContext.Clients.All.SendAsync("UnauthorizedMessage", @event, cancellationToken);
        }
    }
}
