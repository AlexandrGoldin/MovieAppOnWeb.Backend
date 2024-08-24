using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.ApplicationCore.Interfaces;
using MovieApp.Infrastructure.Identity;
using MovieApp.Infrastructure.Specifications;
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
        private readonly IReadRepository<ApplicationUser> _userRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenClaimsService _tokenClaimsService;

        public AuthenticateEndpoint(SignInManager<ApplicationUser> signInManager,
            IReadRepository<ApplicationUser> userRepository, ITokenClaimsService tokenClaimsService)
        {
            _signInManager = signInManager;
            _userRepository = userRepository;
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
            var userFromSigninMenager = await _signInManager.PasswordSignInAsync(request.Username ?? "", request.Password ?? "", false, true);

            if (!userFromSigninMenager.Succeeded)
                throw new ApplicationException("Invalid passwort end/or username.");

            var spec = new UserDetailsSpecification(request.Username, request.Password);
            var userFromRepository = await _userRepository.FirstOrDefaultAsync(spec,
                cancellationToken);

            var AvatarUriDEfault = "/images/account/1.Avatar_Default_Img.png";

            response.Result = userFromSigninMenager.Succeeded;
            response.IsLockedOut = userFromSigninMenager.IsLockedOut;
            response.IsNotAllowed = userFromSigninMenager.IsNotAllowed;
            response.RequiresTwoFactor = userFromSigninMenager.RequiresTwoFactor;
            response.Username = request.Username!;
            response.AvatarUri = userFromRepository?.AvatarUri ?? AvatarUriDEfault;
            response.PhoneNumber = userFromRepository?.PhoneNumber ?? "";

            response.Token = await _tokenClaimsService.GetTokenAsync(request.Username!);
            
            return response;    
        }
    }
}
