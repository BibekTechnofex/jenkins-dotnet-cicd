namespace TransferMock.Controllers.Grpc.Service;

public class AccountStatementDto
{
    public string TransactionId { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Amount { get; set; } 
    public string TransactionType { get; set; } = string.Empty;
    public string TransactionDate { get; set; } = string.Empty;
    public double Balance { get; set; }
}
