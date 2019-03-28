using MediatR;
using System;

namespace SignalRModel.Events
{
    public class AuthenticatedHubEvent : Notification, INotification
    {
    }
}
