
using Microsoft.AspNetCore.Http;

namespace MT.Infrastructure.Utilities;

public static class Tools
{
    public static string? GetCookieValue(IRequestCookieCollection cookie, string cookieName)
    {
        if (cookie. TryGetValue(cookieName, out var cookieValue))
        {
            return cookieValue;
        }
        return null;
    }
}