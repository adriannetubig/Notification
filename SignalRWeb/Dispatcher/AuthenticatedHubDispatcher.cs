using MediatR;
using Microsoft.AspNetCore.SignalR;
using SignalRModel.Events;
using SignalRWeb.Hubs;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRWeb.Dispatcher
{
    public class AuthenticatedHubDispatcher : INotificationHandler<AuthenticatedHubEvent>
    {
        private readonly IHubContext<AuthenticatedHub> _hubContext;
        public AuthenticatedHubDispatcher(IHubContext<AuthenticatedHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public Task Handle(AuthenticatedHubEvent @event, CancellationToken cancellationToken)
        {
            return _hubContext.Clients.All.SendAsync("AuthorizedMessage", @event, cancellationToken);
        }
    }
}
