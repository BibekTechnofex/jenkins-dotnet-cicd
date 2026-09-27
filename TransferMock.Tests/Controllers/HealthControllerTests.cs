using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using TransferMock.Controllers;
using TransferMock.Data;

namespace TransferMock.Tests.Controllers;

public class HealthControllerTests
{
    [Fact]
    public async Task CheckDatabase_WhenDatabaseIsAvailable_ReturnsOkHealthy()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var dbContext = new AppDbContext(options);
        var controller = new HealthController(dbContext);

        var result = await controller.CheckDatabase(CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        Assert.NotNull(okResult.Value);
    }
}
