using MediatR;

namespace SignalRModel.Events
{
    public class UnauthenticatedHubEvent : Notification, INotification
    {
    }
}
