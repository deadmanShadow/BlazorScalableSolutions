using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace BlazorAuth.AppSettings
{
    public class CustomAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, ClaimTypes.Actor)
            };
            return await Task.FromResult(AuthenticateResult.Success
            (new AuthenticationTicket(new ClaimsPrincipal
                    (new ClaimsIdentity(claims, "CustomAuth")),
                Scheme.Name
            )));
        }
    }
}
