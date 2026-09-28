namespace TransferMock.Controllers.Grpc.Service;

public interface IAccountStatementService
{
    Task<IReadOnlyList<AccountStatementDto>> GetAccountStatementsAsync(string accountId);
    Task<IReadOnlyList<AccountStatementDto>> GetAllAccountStatementsAsync();
}
