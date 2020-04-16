
using AuthService.Domain;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System;

namespace AuthService.Service
{
    public interface IAuthService
    {
        public LoggedInUser login(string username, string password);

        public LoggedInUser getLoginedUser(string token);
    }

    public class AuthService : IAuthService
    {
        private IConfiguration config;

        private SymmetricSecurityKey securityKey;
        private SigningCredentials credentials;
        private TokenValidationParameters validator;
        public AuthService(IConfiguration config)
        {
            this.config = config;
            securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["auth:sk"]));
            credentials = new SigningCredentials(securityKey, config["auth:algorithm"]);
            validator = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = securityKey
            };
        }

        public LoggedInUser getLoginedUser(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, validator, out SecurityToken validatedToken);
            var data = validatedToken as JwtSecurityToken;
            return new LoggedInUser(
                data.Payload["username"].ToString(),
                token,
                data.Payload["role"].ToString()
            );
        }

        public LoggedInUser login(string username, string password)
        {
            string token = getToken(username);
            return new LoggedInUser(username, token);
        }

        public string getToken(string username)
        {
            Claim[] claims = new[] {
                new Claim("username", username),
                new Claim("role", "admin"),
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials = credentials
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}