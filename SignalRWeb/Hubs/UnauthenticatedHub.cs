using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace SignalRWeb.Hubs
{
    [EnableCors("CORS")]
    public class UnauthenticatedHub : Hub
    {
    }
}
