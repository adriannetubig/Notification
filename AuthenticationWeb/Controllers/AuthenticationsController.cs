using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AuthenticationFunction;
using AuthenticationModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IFAuthentication _iFAuthentication;
        private readonly IFUser _iFUser;
        private IConfiguration Configuration { get; }
        public AuthenticationsController(IConfiguration configuration, IFAuthentication iFAuthentication, IFUser iFUser)
        {
            Configuration = configuration;
            _iFAuthentication = iFAuthentication;
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
            if (!_iFUser.Login(user))
                return Unauthorized();

            return Ok(_iFAuthentication.Create(user));
        }
    }
}
