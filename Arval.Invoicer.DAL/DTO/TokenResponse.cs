using System.Text.Json.Serialization;

namespace Arval.Invoicer.DAL.DTO;

internal sealed class TokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = null!;

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = null!;

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    public DateTime ObtainedAt { get; } = DateTime.UtcNow;
}