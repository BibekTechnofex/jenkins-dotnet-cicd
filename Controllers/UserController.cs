using Microsoft.AspNetCore.Mvc;
using TransferMock.Controllers.Grpc.Service;

namespace TransferMock.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(UserDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDetailDto>> GetUserDetail(
        string userId,
        [FromQuery] int userType = 0)
    {
        var userDetail = await _userService.GetUserDetailAsync(userId, userType);

        if (userDetail is null)
        {
            return NotFound(new { message = $"User '{userId}' not found." });
        }

        return Ok(userDetail);
    }

    [HttpGet("{userId}/fullname")]
    [ProducesResponseType(typeof(GetUserFullNameResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetUserFullNameResponse>> GetFullName(
        string userId,
        [FromQuery] int userType = 0)
    {
        var fullName = await _userService.GetFullNameAsync(userId, userType);

        if (fullName is null)
        {
            return NotFound(new { message = $"User '{userId}' not found." });
            
        }

        return Ok(new GetUserFullNameResponse { FullName = fullName });
    }
}

public class GetUserFullNameResponse
{
    public string? FullName { get; set; }
}
