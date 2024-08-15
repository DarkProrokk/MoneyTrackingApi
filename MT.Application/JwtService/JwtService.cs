using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using MT.Application.JwtService.Interfaces;
using MT.Application.UserService.Interfaces;

namespace MT.Application.JwtService;

public class JwtService(): IJwtService
{
    public Guid? GetGuidFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtToken;

        try
        {
            jwtToken = handler.ReadJwtToken(jwt);
        }
        catch (Exception)
        {
            return null;
        }

        var guidClaim = jwtToken.Claims.FirstOrDefault(claim =>
            claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

        if (guidClaim == null) return null;

        if (Guid.TryParse(guidClaim.Value, out var guid)) return guid;
        return null;
    }
}