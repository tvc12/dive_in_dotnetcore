using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AuthService.Service;
using CatBasicExample.Domain.Exception;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace CatBasicExample.Controllers.Filter
{
    public class AuthHandler : AuthenticationHandler<AuthOptions>
    {
        private IAuthService service;
        private IHttpContextAccessor httpContextAccessor;

        public AuthHandler(IOptionsMonitor<AuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IAuthService service, IHttpContextAccessor httpContextAccessor) : base(options, logger, encoder, clock)
        {
            this.service = service;
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(HeaderNames.Authorization, out var token))
            {
                throw new UnAuthorized();
            }
            try
            {
                var user = service.getLoginedUser(token);
                httpContextAccessor.HttpContext.Items["LoggedUser"] = user;
                var claims = new[] { new Claim("username", user.Username), new Claim("role", user.Role) };
                var identities = new List<ClaimsIdentity> { new ClaimsIdentity(claims, "jwt") };
                var ticket = new AuthenticationTicket(new ClaimsPrincipal(identities), Options.Scheme);
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
            catch
            {
                throw new UnAuthorized();
            }


        }
    }
}