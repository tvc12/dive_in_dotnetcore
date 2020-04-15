
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
        public LoginedUser login(string username, string password);

        public LoginedUser getLoginedUser(string token);
    }

    public class AuthService : IAuthService
    {
        private IConfiguration config;

        private SymmetricSecurityKey securityKey;
        private SigningCredentials credentials;

        public AuthService(IConfiguration config)
        {
            this.config = config;
            securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["auth:sk"]));
            credentials = new SigningCredentials(securityKey, config["auth:algorithm"]);
        }

        public LoginedUser getLoginedUser(string token)
        {
            throw new System.NotImplementedException();
        }

        public LoginedUser login(string username, string password)
        {
            string token = getToken(username);
            return new LoginedUser(username, token);
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