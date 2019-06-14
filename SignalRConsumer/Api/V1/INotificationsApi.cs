using SignalRModel;
using System.Threading.Tasks;

namespace SignalRConsumer.Api.V1
{
    public interface INotificationsApi
    {
        Task SendMessageToUnauthenticatedConsumer(Notification notification);
        Task SendMessageToAuthenticatedConsumer(string jwtToken, Notification notification);
    }
}
