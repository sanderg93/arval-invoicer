namespace Arval.Invoicer.DAL.DTO;

public sealed class ChargeHistoryResponse
{
    public int Pages { get; set; }
    public List<ChargeSession> Data { get; set; } = new List<ChargeSession>();
    public string Message { get; set; } = null!;
}
