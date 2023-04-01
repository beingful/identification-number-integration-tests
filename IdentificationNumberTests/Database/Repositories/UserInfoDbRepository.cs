using Microsoft.EntityFrameworkCore;
using IntegrationTests.Database.Models;

namespace IntegrationTests.Database.Repositories;

internal sealed class UserInfoDbRepository
{
    private readonly UserInfoDbContext _userInfoDbContext;

    public UserInfoDbRepository(UserInfoDbContext userInfoDbContext) =>
        _userInfoDbContext = userInfoDbContext;

    public async Task<UserInfoContext?> GetUserInfoAsync(string id) =>
        await _userInfoDbContext
            .UsersInfo
            .FirstOrDefaultAsync(userInfo => userInfo.Id == id);
}
