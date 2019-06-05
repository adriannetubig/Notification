using AuthenticationModel;
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
        private readonly Authorization _authorization;
        public FAuthentication(Authorization authorization)
        {
            _authorization = authorization;
        }
        public Authentication Create(string refreshToken, User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authorization.IssuerSigningKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, "Manager")
            };

            var authenticationResult = new Authentication
            {
                Expiration = _authorization.Expiration,
                InvalidBefore = DateTime.UtcNow,
                RefreshToken = refreshToken
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: _authorization.ValidIssuer,
                audience: _authorization.ValidAudience,
                claims: claims,
                notBefore: authenticationResult.InvalidBefore,
                expires: authenticationResult.Expiration,
                signingCredentials: signinCredentials
            );
            authenticationResult.Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return authenticationResult;
        }

        public User GetUserDetailsFromToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authorization.IssuerSigningKey)),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return new User
            {
                UserId = Convert.ToInt32(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                UserName = principal.Identity.Name
            };
        }
    }
}
