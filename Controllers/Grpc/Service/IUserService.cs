namespace TransferMock.Controllers.Grpc.Service;

public interface IUserService
{
    Task<UserDetailDto?> GetUserDetailAsync(string userId, int userType);
    Task<string?> GetFullNameAsync(string userId, int userType);
}
