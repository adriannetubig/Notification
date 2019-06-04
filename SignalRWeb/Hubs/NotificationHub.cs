using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRWeb.Hubs
{
    [EnableCors("CORS")]
    public class NotificationHub : Hub
    {
        public async Task WithoutAuthorization(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
