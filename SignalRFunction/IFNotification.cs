using SignalRModel;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRFunction
{
    public interface IFNotification
    {
        Task SendMessageToAuthenticatedConsumer(Notification notification, CancellationToken cancellationToken);
    }
}
