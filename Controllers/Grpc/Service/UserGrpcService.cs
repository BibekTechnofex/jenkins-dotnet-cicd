using Grpc.Core;
using User;

namespace TransferMock.Controllers.Grpc.Service;

public class UserGrpcService : global::User.UserService.UserServiceBase
{
    private readonly IUserService _userService;

    public UserGrpcService(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task<GetUserResponse> GetFullName(
        GetUserRequest request,
        ServerCallContext context)
    {
        var userDetail = await _userService.GetUserDetailAsync(
            request.UserId,
            request.UserType);

        if (userDetail is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"User '{request.UserId}' not found."));
        }

        return new GetUserResponse
        {
            FullName = userDetail.FullName
        };
    }
}
