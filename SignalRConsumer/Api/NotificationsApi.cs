using SignalRModel;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SignalRConsumer.Api
{
    public class NotificationsApi: INotificationsApi
    {
        private string _uRLSignalR;

        public NotificationsApi(string uRLSignalR)
        {
            _uRLSignalR = uRLSignalR;
        }
        public async Task SendMessageToUnauthenticatedConsumer(Notification notification)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_uRLSignalR);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync("api/Notifications/SendMessageToUnauthenticatedConsumer", notification);
            response.EnsureSuccessStatusCode();
        }
    }
}
