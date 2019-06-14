using BaseConsumer;
using SignalRModel;
using System.Threading.Tasks;

namespace SignalRConsumer.Api.V1
{
    public class NotificationsApi: BaseApi, INotificationsApi
    {
        const string _version = "V1";
        public NotificationsApi(string url) : base(url) { }

        public async Task SendMessageToUnauthenticatedConsumer(Notification notification)
        {
            await Post(notification, $"Api/{_version}/Notifications/SendMessageToUnauthenticatedConsumer");
        }
        public async Task SendMessageToAuthenticatedConsumer(string jwtToken,Notification notification)
        {
            await Post(notification, $"Api/{_version}/Notifications/SendMessageToAuthenticatedConsumer", jwtToken);
        }
    }
}
