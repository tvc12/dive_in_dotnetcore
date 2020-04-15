using System.Threading.Tasks;
using AuthService.Domain;
using AuthService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controller {
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase {
        private IAuthService service;

        public AuthController(IAuthService service)
        {
            this.service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public Task<LoginedUser> login([FromQuery] string username, [FromQuery] string password) {
            return Task.Run(() => service.login(username, password));
        }
    }
}