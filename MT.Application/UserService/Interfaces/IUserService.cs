using MT.Application.BaseService.Interfaces;
using MT.Domain.Entity;

namespace MT.Application.UserService.Interfaces;

public interface IUserService: IBaseService<User>
{
    public Task<User?> GetActiveUserFromJwt(string jwt);
}