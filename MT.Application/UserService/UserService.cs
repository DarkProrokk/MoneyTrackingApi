using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging.Abstractions;
using MT.Application.BaseService;
using MT.Application.JwtService.Interfaces;
using MT.Application.UserService.Interfaces;
using MT.Application.UserService.Specification;
using MT.Domain.Entity;
using MT.Infrastructure.Data.Repository.Interfaces;

namespace MT.Application.UserService;

public class UserService(IUserRepository userRepository, IJwtService jwtService) : BaseService<User>(userRepository), IUserService
{
    public async Task<User?> GetActiveUserFromJwt(string jwt)
    {
        var guid = jwtService.GetGuidFromJwt(jwt);
        if (guid is null) return null;
        var user = await base.GetByGuidAsync(guid.Value);
        return user;
    }
}