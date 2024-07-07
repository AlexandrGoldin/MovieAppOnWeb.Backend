using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.ApplicationCore.Interfaces;
using MovieApp.Infrastructure.Identity;
using Swashbuckle.AspNetCore.Annotations;

namespace MovieApp.WebApi.AuthEndpoints
{
    /// <summary>
    /// Authenticates a user
    /// </summary>
    public class AuthenticateEndpoint : EndpointBaseAsync
        .WithRequest<AuthenticateRequest>
        .WithActionResult<AuthenticateResponse>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenClaimsService _tokenClaimsService;

        public AuthenticateEndpoint(SignInManager<ApplicationUser> signInManager,
            ITokenClaimsService tokenClaimsService)
        {
            _signInManager = signInManager;
            _tokenClaimsService = tokenClaimsService;
        }

        [HttpPost("api/authenticate")]
        [SwaggerOperation(
            Summary = "Authenticates a user",
            Description = "Authenticates a user",
            OperationId = "auth.authenticate",
            Tags = new[] { "AuthEndpoints" })
        ]
        public override async Task<ActionResult<AuthenticateResponse>> HandleAsync(AuthenticateRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = new AuthenticateResponse();

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            //var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
            var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, true);

            if (!result.Succeeded)
                throw new ApplicationException($"Unknown passwort: {request.Password} or username: {request.Username}.");

            response.Result = result.Succeeded;
            response.IsLockedOut = result.IsLockedOut;
            response.IsNotAllowed = result.IsNotAllowed;
            response.RequiresTwoFactor = result.RequiresTwoFactor;
            response.Username = request.Username;

            response.Token = await _tokenClaimsService.GetTokenAsync(request.Username);
            
            return response;    
        }
    }
}
