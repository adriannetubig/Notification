using AuthenticationModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationFunction
{
    public class FAuthentication: IFAuthentication
    {
        private IConfiguration Configuration { get; }
        public FAuthentication(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public Authentication Create(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Authentication:IssuerSigningKey").Value));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Manager")
            };

            var authenticationResult = new Authentication
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

            return authenticationResult;
        }
    }
}
