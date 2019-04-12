using SignalRModel;
using System.Threading.Tasks;

namespace SignalRConsumer.Api
{
    public interface INotificationsApi
    {
        Task SendMessageToUnauthenticatedConsumer(Notification notification);
    }
}
