using SignalRModel;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SignalRConsumer.Api
{
    public class NotificationsApi: INotificationsApi
    {
        private readonly string _url;

        public NotificationsApi(string url)
        {
            _url = url;
        }
        public async Task SendMessageToUnauthenticatedConsumer(Notification notification)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_url);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/Notifications/SendMessageToUnauthenticatedConsumer", notification);
            response.EnsureSuccessStatusCode();
        }
        public async Task SendMessageToAuthenticatedConsumer(string jwtToken,Notification notification)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_url);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/Notifications/SendMessageToAuthenticatedConsumer", notification);
            response.EnsureSuccessStatusCode();
        }
    }
}
