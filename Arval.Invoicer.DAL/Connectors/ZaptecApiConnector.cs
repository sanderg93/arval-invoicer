using System.Text.Json;
using Arval.Invoicer.DAL.DTO;

namespace Arval.Invoicer.DAL;
public sealed partial class ZaptecApiConnector
{
    private readonly HttpClient _httpClient;
    private readonly ZaptecConfiguration _configuration;
    private TokenResponse? _accessToken;

    public ZaptecApiConnector(HttpClient httpClient, ZaptecConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public Task<ChargeHistoryResponse> GetChargeHistoryAsync()
    {
        return GetChargeHistoryAsync(new ChargeHistoryParameters());
    }

    public async Task<ChargeHistoryResponse> GetChargeHistoryAsync(ChargeHistoryParameters parameters)
    {
        var queryParams = new List<KeyValuePair<string, string>>();

        // Adding all properties from parameters to query string
        if (parameters.InstallationId.HasValue) queryParams.Add(new KeyValuePair<string, string>("InstallationId", parameters.InstallationId.Value.ToString()));
        if (parameters.UserId.HasValue) queryParams.Add(new KeyValuePair<string, string>("UserId", parameters.UserId.Value.ToString()));
        if (parameters.ChargerId.HasValue) queryParams.Add(new KeyValuePair<string, string>("ChargerId", parameters.ChargerId.Value.ToString()));
        if (parameters.From.HasValue) queryParams.Add(new KeyValuePair<string, string>("From", parameters.From.Value.ToString("o")));
        if (parameters.To.HasValue) queryParams.Add(new KeyValuePair<string, string>("To", parameters.To.Value.ToString("o")));
        if (parameters.GroupBy.HasValue) queryParams.Add(new KeyValuePair<string, string>("GroupBy", parameters.GroupBy.Value.ToString()));
        if (parameters.DetailLevel.HasValue) queryParams.Add(new KeyValuePair<string, string>("DetailLevel", parameters.DetailLevel.Value.ToString()));
        if (!string.IsNullOrEmpty(parameters.SortProperty)) queryParams.Add(new KeyValuePair<string, string>("SortProperty", parameters.SortProperty));
        if (parameters.SortDescending.HasValue) queryParams.Add(new KeyValuePair<string, string>("SortDescending", parameters.SortDescending.Value.ToString()));
        if (parameters.PageSize.HasValue) queryParams.Add(new KeyValuePair<string, string>("PageSize", parameters.PageSize.Value.ToString()));
        if (parameters.PageIndex.HasValue) queryParams.Add(new KeyValuePair<string, string>("PageIndex", parameters.PageIndex.Value.ToString()));
        if (parameters.IncludeDisabled.HasValue) queryParams.Add(new KeyValuePair<string, string>("IncludeDisabled", parameters.IncludeDisabled.Value.ToString()));
        if (parameters.Exclude != null && parameters.Exclude.Any()) queryParams.Add(new KeyValuePair<string, string>("Exclude", string.Join(",", parameters.Exclude)));

        var queryString = new FormUrlEncodedContent(queryParams).ReadAsStringAsync().Result;
        var url = $"/api/chargehistory?{queryString}";

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await GetToken());

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            // Handle errors as appropriate.
            // You might want to throw an exception or return a null or a default value.
        }

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ChargeHistoryResponse>(content) ?? throw new Exception("Deserialization of content failed");
    }

    private async Task<string> GetToken()
    {
        if (IsTokenExpired()) await FetchTokenAsync();
        return _accessToken!.AccessToken;
    }

    private bool IsTokenExpired()
    {
        if (_accessToken == null) return true;

        var tokenExpirationTime = _accessToken.ObtainedAt.AddSeconds(_accessToken.ExpiresIn).AddMinutes(-2);
        return DateTime.UtcNow >= tokenExpirationTime;
    }

    private async Task FetchTokenAsync()
    {
        var tokenRequestContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("username", _configuration.Username),
            new KeyValuePair<string, string>("password", _configuration.Password)
        });

        var tokenResponse = await _httpClient.PostAsync(_configuration.TokenUrl, tokenRequestContent);
        if (tokenResponse.IsSuccessStatusCode)
        {
            var content = await tokenResponse.Content.ReadAsStringAsync();
            _accessToken = JsonSerializer.Deserialize<TokenResponse>(content);
            return;
        }

        _accessToken = null;
        throw new InvalidOperationException("Authentication failed while trying to fetch a new token.");
    }

}
