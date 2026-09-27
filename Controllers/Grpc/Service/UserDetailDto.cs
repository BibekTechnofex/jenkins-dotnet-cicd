namespace TransferMock.Controllers.Grpc.Service;

public class UserDetailDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int UserType { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
