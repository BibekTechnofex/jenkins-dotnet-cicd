using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using TransferMock.Controllers;
using TransferMock.Controllers.Grpc.Service;

namespace TransferMock.Tests.Controllers;

public class UserControllerTests
{
    [Fact]
    public async Task GetUserDetail_WhenUserExists_ReturnsOkWithUser()
    {
        var expected = new UserDetailDto
        {
            Id = 1,
            UserId = "U001",
            UserType = 0,
            FullName = "Jane Doe",
            CreatedAt = new DateTime(2026, 8, 1, 0, 0, 0, DateTimeKind.Utc)
        };

        var userService = new Mock<IUserService>();
        userService
            .Setup(service => service.GetUserDetailAsync("U001", 0))
            .ReturnsAsync(expected);

        var controller = new UserController(userService.Object);

        var result = await controller.GetUserDetail("U001");

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        var actual = Assert.IsType<UserDetailDto>(okResult.Value);
        Assert.Equal(expected.UserId, actual.UserId);
        Assert.Equal(expected.FullName, actual.FullName);
        userService.Verify(service => service.GetUserDetailAsync("U001", 0), Times.Once);
    }

    [Fact]
    public async Task GetFullName_WhenUserExists_ReturnsOkWithFullName()
    {
        var userService = new Mock<IUserService>();
        userService
            .Setup(service => service.GetFullNameAsync("U001", 0))
            .ReturnsAsync("Jane Doe");

        var controller = new UserController(userService.Object);

        var result = await controller.GetFullName("U001");

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        var actual = Assert.IsType<GetUserFullNameResponse>(okResult.Value);
        Assert.Equal("Jane Doe", actual.FullName);
        userService.Verify(service => service.GetFullNameAsync("U001", 0), Times.Once);
    }
}
