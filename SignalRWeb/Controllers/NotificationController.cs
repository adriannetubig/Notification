using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SignalRWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        [AllowAnonymous, HttpPost("NoAuthorization")]
        public IActionResult NoAuthorization()
        {
            return Ok("NoAuthorization");
        }

        [Authorize, HttpPost("WithAuthorization")]
        public IActionResult WithAuthorization()
        {
            return Ok(User.Identities.FirstOrDefault().Name);
        }
    }
}
