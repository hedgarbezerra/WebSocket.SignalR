using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebSocket.SignalR.Models;

namespace WebSocket.SignalR.Controllers
{
    public class BaseAuthenticatedController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public BaseAuthenticatedController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        protected AppUser GetAuthentcatedUser()
        {
            var identity = HttpContext.User.Identity;
            if (identity is null)
                throw new UnauthorizedAccessException("Unauthorized access terminated.");

            var user = _userManager.FindByEmailAsync(identity.Name).Result;

            return user;
        }
    }
}
