using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRWeb.Hubs;

namespace SignalRWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationsController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }


        [AllowAnonymous, HttpPost("NoAuthorization")]
        public IActionResult NoAuthorization()
        {
            Task.WaitAll(
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "Api", "NoAuthorization")
            );
            return Ok("NoAuthorization");
        }

        [Authorize, HttpPost("WithAuthorization")]
        public IActionResult WithAuthorization()
        {
            Task.WaitAll(
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "Api", User.Identities.FirstOrDefault().Name)
            );
            return Ok(User.Identities.FirstOrDefault().Name);
        }
    }
}
