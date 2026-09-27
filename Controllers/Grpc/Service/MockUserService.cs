using Microsoft.EntityFrameworkCore;
using TransferMock.Data;

namespace TransferMock.Controllers.Grpc.Service;

public class MockUserService : IUserService
{
    private readonly AppDbContext _dbContext;

    public MockUserService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDetailDto?> GetUserDetailAsync(string userId, int userType)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserId == userId && u.UserType == userType);

        if (user is null)
        {
            return null;
        }

        return new UserDetailDto
        {
            Id = user.Id,
            UserId = user.UserId,
            UserType = user.UserType,
            FullName = user.FullName,
            CreatedAt = user.CreatedAt
        };
    }

    public async Task<string?> GetFullNameAsync(string userId, int userType)
    {
        var userDetail = await GetUserDetailAsync(userId, userType);
        return userDetail?.FullName;
    }
}
