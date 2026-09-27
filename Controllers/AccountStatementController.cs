using Microsoft.AspNetCore.Mvc;
using TransferMock.Controllers.Grpc.Service;

namespace TransferMock.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountStatementController : ControllerBase
{
    private readonly IAccountStatementService _accountStatementService;

    public AccountStatementController(IAccountStatementService accountStatementService)
    {
        _accountStatementService = accountStatementService;
    }

    [HttpGet("{accountId}")]
    [ProducesResponseType(typeof(IReadOnlyList<AccountStatementDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<AccountStatementDto>>> GetAccountStatements(string accountId)
    {
        var statements = await _accountStatementService.GetAccountStatementsAsync(accountId);
        return Ok(statements);
    }
}
