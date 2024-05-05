using Asp.Versioning;
using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using WebSocket.SignalR.Extensions;
using WebSocket.SignalR.Models;
using WebSocket.SignalR.Models.DTOs;

namespace WebSocket.SignalR.Controllers
{
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private static readonly EmailAddressAttribute _emailAddressAttribute = new();

        private readonly UserManager<AppUser> _userManager;
        private readonly IUserStore<AppUser> _userStore;

        public IdentityController(UserManager<AppUser> userManager, IUserStore<AppUser> userStore)
        { 
            ArgumentNullException.ThrowIfNull(userManager);
            ArgumentNullException.ThrowIfNull(userStore);

            _userManager = userManager;
            _userStore = userStore;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO registration)
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException($"{nameof(AppUser)} requires a user store with email support.");
            }

            var email = registration.Email;
            var emailStore = (IUserEmailStore<AppUser>)_userStore;

            if (string.IsNullOrEmpty(email) || !_emailAddressAttribute.IsValid(email))
            {
                var emailFailedResult = IdentityResult.Failed(_userManager.ErrorDescriber.InvalidEmail(email)).FromIdentityResult();
                return BadRequest(emailFailedResult);
            }

            var user = new AppUser() { Birthdate = registration.Birthdate.Value, Name = registration.Name };
            await _userStore.SetUserNameAsync(user, email, CancellationToken.None);
            await emailStore.SetEmailAsync(user, email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, registration.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.FromIdentityResult());
            }

            return Created(HttpContext.Request.Path, result.FromIdentityResult());
        }
    }
}
