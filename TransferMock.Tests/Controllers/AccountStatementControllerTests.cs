using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using TransferMock.Controllers;
using TransferMock.Controllers.Grpc.Service;

namespace TransferMock.Tests.Controllers;

public class AccountStatementControllerTests
{
    [Fact]
    public async Task GetAccountStatements_WhenStatementsExist_ReturnsOkWithStatements()
    {
        var expected = new List<AccountStatementDto>
        {
            new()
            {
                TransactionId = "TXN-ACC001-001",
                Description = "Opening balance",
                Amount = 10000.00,
                TransactionType = "Credit",
                TransactionDate = "2026-08-01",
                Balance = 10000.00
            },
            new()
            {
                TransactionId = "TXN-ACC001-002",
                Description = "Salary deposit",
                Amount = 25000.00,
                TransactionType = "Credit",
                TransactionDate = "2026-08-02",
                Balance = 35000.00
            }
        };

        var accountStatementService = new Mock<IAccountStatementService>();
        accountStatementService
            .Setup(service => service.GetAccountStatementsAsync("ACC001"))
            .ReturnsAsync(expected);

        var controller = new AccountStatementController(accountStatementService.Object);

        var result = await controller.GetAccountStatements("ACC001");

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        var actual = Assert.IsAssignableFrom<IReadOnlyList<AccountStatementDto>>(okResult.Value);
        Assert.Equal(2, actual.Count);
        Assert.Equal("TXN-ACC001-001", actual[0].TransactionId);
        Assert.Equal(35000.00, actual[1].Balance);
        accountStatementService.Verify(
            service => service.GetAccountStatementsAsync("ACC001"),
            Times.Once);
    }
}
