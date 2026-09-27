using Accountstatement;
using Grpc.Core;

namespace TransferMock.Controllers.Grpc.Service;

public class AccountStatementGrpcService : global::Accountstatement.AccountStatementService.AccountStatementServiceBase
{
    private readonly IAccountStatementService _accountStatementService;

    public AccountStatementGrpcService(IAccountStatementService accountStatementService)
    {
        _accountStatementService = accountStatementService;
    }

    public override async Task GetAccountStatements(
        GetAccountStatementsRequest request,
        IServerStreamWriter<AccountStatementItem> responseStream,
        ServerCallContext context)
    {
        var statements = await _accountStatementService.GetAccountStatementsAsync(request.AccountId);

        foreach (var statement in statements)
        {
            if (context.CancellationToken.IsCancellationRequested)
            {
                break;
            }

            await responseStream.WriteAsync(new AccountStatementItem
            {
                TransactionId = statement.TransactionId,
                Description = statement.Description,
                Amount = statement.Amount,
                TransactionType = statement.TransactionType,
                TransactionDate = statement.TransactionDate,
                Balance = statement.Balance
            });

            await Task.Delay(200, context.CancellationToken);
        }
    }
}
