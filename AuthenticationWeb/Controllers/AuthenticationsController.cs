using System.Linq;
using AuthenticationFunction;
using AuthenticationModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationWeb.Controllers
{
    [EnableCors("CORS")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IFAuthentication _iFAuthentication;
        private readonly IFRefreshToken _iFRefreshToken;
        private readonly IFUser _iFUser;
        public AuthenticationsController(IFAuthentication iFAuthentication, IFRefreshToken iFRefreshToken, IFUser iFUser)
        {
            _iFAuthentication = iFAuthentication;
            _iFRefreshToken = iFRefreshToken;
            _iFUser = iFUser;
        }


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

        [AllowAnonymous ,HttpPost, Route("Login")]
        public IActionResult Login([FromBody]User user)
        {
            user = _iFUser.Login(user);
            if (user.UserId != 1)
                return Unauthorized();

            var refreshToken = _iFRefreshToken.Create(user.UserId);

            return Ok(_iFAuthentication.Create(refreshToken, user));
        }

        [AllowAnonymous, HttpPost, Route("Refresh")]
        public IActionResult Refresh([FromBody]Authentication authentication)
        {
            var user = _iFAuthentication.GetUserDetailsFromToken(authentication.Token);
            if (user.UserId != 1 && !_iFRefreshToken.IsValidRefreshToken(user.UserId, authentication.RefreshToken))
                return Unauthorized();

            var refreshToken = _iFRefreshToken.Create(user.UserId);

            return Ok(_iFAuthentication.Create(refreshToken, user));
        }
    }
}
