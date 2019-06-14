using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace SignalRWeb.Controllers
{
    [EnableCors("CORS")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected virtual string UserName => User.Identities.FirstOrDefault().Name;
    }
}
