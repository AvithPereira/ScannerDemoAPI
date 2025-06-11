using Microsoft.AspNetCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using ScannerDemoAPI.Data;

namespace ScannerDemoAPI.Controllers
{
    [Route("api/scannerdemo")]
    [ApiController]
    public class AuthorizationController(ApplicationDbContext db) : Controller
    {
        private readonly ApplicationDbContext _db = db;

        [HttpPost("token")]
        public async Task<IActionResult> Exchange()
        {
            var request = HttpContext.GetOpenIddictServerRequest();
            if (request == null)
                return BadRequest();

            // Validate user credentials from your UserInfo table  
            var user = await _db.UserInfos
                .FirstOrDefaultAsync(u => u.Name == request.Username && u.Password == request.Password);

            if (user == null)
            {
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new Microsoft.AspNetCore.Authentication.AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictConstants.Parameters.Error] = OpenIddictConstants.Errors.InvalidGrant,
                        [OpenIddictConstants.Parameters.ErrorDescription] = "Invalid credentials."
                    }));
            }

            var identity = new ClaimsIdentity(
                OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                OpenIddictConstants.Claims.Name, null);

            identity.AddClaim(OpenIddictConstants.Claims.Subject, user.UserID.ToString());
            identity.AddClaim(OpenIddictConstants.Claims.Name, user.Name);

            var principal = new ClaimsPrincipal(identity);
            principal.SetScopes(request.GetScopes());

            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
    }
}
