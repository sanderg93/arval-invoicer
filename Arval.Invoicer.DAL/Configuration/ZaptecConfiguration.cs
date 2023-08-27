namespace Arval.Invoicer.DAL;

public sealed class ZaptecConfiguration
{
    public string BaseAddress { get; set; } = null!;
    public string TokenUrl { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}