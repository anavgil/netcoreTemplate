using System.Security.Claims;
using System.Text.Encodings.Web;
using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authentication.Apikey;

public class ApiKeySchemeHandler : AuthenticationHandler<ApiKeySchemeOptions>
{
    private readonly IdentityContext _context;

    public ApiKeySchemeHandler(IdentityContext context, IOptionsMonitor<ApiKeySchemeOptions> options,
        ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder)
    {
        _context = context;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(Options.HeaderName, out Microsoft.Extensions.Primitives.StringValues headerValue))
        {
            return AuthenticateResult.Fail("Header Not Found.");
        }

        var apiKey = await Task.FromResult(string.Empty);
        //var apiKey = await _context.ApiKeys
        //    .AsNoTracking() // TODO: Usar caché es buena idea
        //    .FirstOrDefaultAsync(a => a.Key.ToString() == headerValue);

        if (apiKey is null)
        {
            return AuthenticateResult.Fail("Wrong Api Key.");
        }

        var claims = new Claim[]
        {
            //new(ClaimTypes.NameIdentifier, $"{apiKey.ApiKeyId}"),
            //new(ClaimTypes.Name, apiKey.Name)
        };

        var identiy = new ClaimsIdentity(claims, nameof(ApiKeySchemeHandler));
        var principal = new ClaimsPrincipal(identiy);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}
