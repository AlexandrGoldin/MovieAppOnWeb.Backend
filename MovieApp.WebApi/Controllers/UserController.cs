using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Infrastructure.Identity;

namespace MovieApp.WebApi.Controllers
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<UserController> _logger;

        public UserController(SignInManager<ApplicationUser> signInManager,
                              ILogger<UserController> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpDelete]
        [Authorize]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            _logger.LogInformation("---User logged out.---");

            return NoContent();
        }

    }
}

        //[HttpGet]
        //[Authorize]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetCurrentUser() =>
        //    Ok(await CreateUserInfo(User));

        //[Route("Logout")]
        //[HttpPost]
        //[Authorize]
        //[AllowAnonymous]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    var userId = _signInManager.Context.User.Claims.First(c => c.Type == ClaimTypes.Name);
        //    var identityKey = _signInManager.Context.Request.Cookies[ConfigureCookieSettings.IdentifierCookieName];
        //    _cache.Set($"{userId.Value}:{identityKey}", identityKey, new MemoryCacheEntryOptions
        //    {
        //        AbsoluteExpiration = DateTime.Now.AddMinutes(ConfigureCookieSettings.ValidityMinutesPeriod)
        //    });

        //    _logger.LogInformation("User logged out.");
        //    return Ok();
        //}

        //private async Task<UserInfo> CreateUserInfo(ClaimsPrincipal claimsPrincipal)
        //{
        //    if (claimsPrincipal.Identity == null || claimsPrincipal.Identity.Name == null || !claimsPrincipal.Identity.IsAuthenticated)
        //    {
        //        return UserInfo.Anonymous;
        //    }

        //    var userInfo = new UserInfo
        //    {
        //        IsAuthenticated = true
        //    };

        //    if (claimsPrincipal.Identity is ClaimsIdentity claimsIdentity)
        //    {
        //        userInfo.NameClaimType = claimsIdentity.NameClaimType;
        //        userInfo.RoleClaimType = claimsIdentity.RoleClaimType;
        //    }
        //    else
        //    {
        //        userInfo.NameClaimType = "name";
        //        userInfo.RoleClaimType = "role";
        //    }

        //    if (claimsPrincipal.Claims.Any())
        //    {
        //        var claims = new List<ClaimValue>();
        //        var nameClaims = claimsPrincipal.FindAll(userInfo.NameClaimType);
        //        foreach (var claim in nameClaims)
        //        {
        //            claims.Add(new ClaimValue(userInfo.NameClaimType, claim.Value));
        //        }

        //        foreach (var claim in claimsPrincipal.Claims.Except(nameClaims))
        //        {
        //            claims.Add(new ClaimValue(claim.Type, claim.Value));
        //        }

        //        userInfo.Claims = claims;
        //    }

        //    var token = await _tokenClaimsService.GetTokenAsync(claimsPrincipal.Identity.Name);
        //    userInfo.Token = token;

        //    return userInfo;
        //}
//    }
//}
