using Microsoft.AspNetCore.Http;

namespace MT.Application.JwtService.Interfaces;

public interface IJwtService
{
    // Guid? GetGuidFromJwtAsync(IRequestCookieCollection request);
    public Guid? GetGuidFromJwt(string jwt);
}