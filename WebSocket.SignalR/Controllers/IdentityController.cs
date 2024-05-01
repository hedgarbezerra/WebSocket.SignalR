using Asp.Versioning;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(UserManager<AppUser> userManager, IUserStore<AppUser> userStore, ILogger<IdentityController> logger)
        { 
            ArgumentNullException.ThrowIfNull(userManager);
            ArgumentNullException.ThrowIfNull(userStore);
            ArgumentNullException.ThrowIfNull(logger);

            _userManager = userManager;
            _userStore = userStore;
            _logger = logger;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO registration)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!_userManager.SupportsUserEmail)
                {
                    throw new NotSupportedException($"{nameof(AppUser)} requires a user store with email support.");
                }

                var email = registration.Email;
                var emailStore = (IUserEmailStore<AppUser>)_userStore;

                if (string.IsNullOrEmpty(email) || !_emailAddressAttribute.IsValid(email))
                {
                    return BadRequest(CreateValidationProblem(IdentityResult.Failed(_userManager.ErrorDescriber.InvalidEmail(email))));
                }

                var user = new AppUser() { Birthdate = registration.Birthdate, Name = registration.Name };
                await _userStore.SetUserNameAsync(user, email, CancellationToken.None);
                await emailStore.SetEmailAsync(user, email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, registration.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(CreateValidationProblem(result));
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }            
        }

        private static ValidationProblem CreateValidationProblem(IdentityResult result)
        {
            // We expect a single error code and description in the normal case.
            // This could be golfed with GroupBy and ToDictionary, but perf! :P
            Debug.Assert(!result.Succeeded);
            var errorDictionary = new Dictionary<string, string[]>(1);

            foreach (var error in result.Errors)
            {
                string[] newDescriptions;

                if (errorDictionary.TryGetValue(error.Code, out var descriptions))
                {
                    newDescriptions = new string[descriptions.Length + 1];
                    Array.Copy(descriptions, newDescriptions, descriptions.Length);
                    newDescriptions[descriptions.Length] = error.Description;
                }
                else
                {
                    newDescriptions = [error.Description];
                }

                errorDictionary[error.Code] = newDescriptions;
            }

            return TypedResults.ValidationProblem(errorDictionary);
        }
    }
}
