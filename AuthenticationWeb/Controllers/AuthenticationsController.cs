using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationWeb.Models;
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
        private IConfiguration Configuration { get; }
        public AuthenticationsController(IConfiguration configuration)
        {
            Configuration = configuration;
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
        public IActionResult Login([FromBody]LoginModel user)
        {
            //ToDo: put this in Business Logic
            //ToDo: do not use static username and password
            if (user == null && (user.UserName != "username" && user.Password != "password"))
                return Unauthorized();

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Authentication:IssuerSigningKey").Value));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Manager")
            };

            var authenticationResult = new AuthenticationResult
            {
                Exiration = DateTime.Now.AddMinutes(Convert.ToDouble(Configuration.GetSection("Authentication:ExpiresMinutes").Value)),
                InvalidBefore = DateTime.Now,
            };

            var tokeOptions = new JwtSecurityToken(
                issuer: Configuration.GetSection("Authentication:ValidIssuer").Value,
                audience: Configuration.GetSection("Authentication:ValidAudience").Value,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(Configuration.GetSection("Authentication:ExpiresMinutes").Value)),
                signingCredentials: signinCredentials
            );
            authenticationResult.Token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(authenticationResult);
        }
    }
}
