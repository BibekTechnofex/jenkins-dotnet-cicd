namespace TransferMock.Controllers.Grpc.Service;

public class MockAccountStatementService : IAccountStatementService
{
  private static readonly Dictionary<string, IReadOnlyList<AccountStatementDto>> StatementsByAccount = new()
    {
        ["ACC001"] = new List<AccountStatementDto>
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
            },
            new()
            {
                TransactionId = "TXN-ACC001-003",
                Description = "Utility bill payment",
                Amount = 1500.00,
                TransactionType = "Debit",
                TransactionDate = "2026-08-03",
                Balance = 33500.00
            },
            new()
            {
                TransactionId = "TXN-ACC001-004",
                Description = "Grocery purchase",
                Amount = 850.50,
                TransactionType = "Debit",
                TransactionDate = "2026-08-04",
                Balance = 32649.50
            },
            new()
            {
                TransactionId = "TXN-ACC001-005",
                Description = "Interest credit",
                Amount = 125.25,
                TransactionType = "Credit",
                TransactionDate = "2026-08-05",
                Balance = 32774.75
            }
        },
        ["ACC002"] = new List<AccountStatementDto>
        {
            new()
            {
                TransactionId = "TXN-ACC002-001",
                Description = "Opening balance",
                Amount = 5000.00,
                TransactionType = "Credit",
                TransactionDate = "2026-08-01",
                Balance = 5000.00
            },
            new()
            {
                TransactionId = "TXN-ACC002-002",
                Description = "Online transfer received",
                Amount = 1200.00,
                TransactionType = "Credit",
                TransactionDate = "2026-08-03",
                Balance = 6200.00
            },
            new()
            {
                TransactionId = "TXN-ACC002-003",
                Description = "ATM withdrawal",
                Amount = 500.00,
                TransactionType = "Debit",
                TransactionDate = "2026-08-05",
                Balance = 5700.00
            }
        }
    };

    public Task<IReadOnlyList<AccountStatementDto>> GetAccountStatementsAsync(string accountId)
    {
        if (StatementsByAccount.TryGetValue(accountId, out var statements))
        {
            return Task.FromResult(statements);
        }

        var mockStatements = new List<AccountStatementDto>
        {
            new()
            {
                TransactionId = $"TXN-{accountId}-001",
                Description = "Mock opening balance",
                Amount = 1000.00,
                TransactionType = "Credit",
                TransactionDate = "2026-08-01",
                Balance = 1000.00
            },
            new()
            {
                TransactionId = $"TXN-{accountId}-002",
                Description = "Mock debit transaction",
                Amount = 250.00,
                TransactionType = "Debit",
                TransactionDate = "2026-08-02",
                Balance = 750.00
            },
            new()
            {
                TransactionId = $"TXN-{accountId}-003",
                Description = "Mock credit transaction",
                Amount = 500.00,
                TransactionType = "Credit",
                TransactionDate = "2026-08-03",
                Balance = 1250.00
            }
        };

        return Task.FromResult<IReadOnlyList<AccountStatementDto>>(mockStatements);
    }
}
