using PINTests.Http.Clients;
using PINTests.Http.JsonSerializer;
using PINTests.Http.Constants;
using PINTests.Variables;
using PINTests.Integration.Token.Models;
using PINTests.Integration.Authorization;

namespace PINTests.Integration.Token;

internal sealed class TokenService
{
    private readonly AuthService _authService;
    private readonly SensitiveData _sensitiveData;
    private readonly FlurlHttpClient _httpClient;

    public TokenService(AuthService authService, SensitiveData sensitiveData)
    {
        _authService = authService;
        _sensitiveData = sensitiveData;

        _httpClient = new FlurlHttpClient(
            baseUrl: Endpoints.TokenEndpoint(_sensitiveData.OrganizationInfo.TenantId),
            serializerSettings: SerializerSettings.SnakeCaseSettings);
    }

    public async Task<string> GetTokenAsync()
    {
        string code = GetCode();

        TokenResponse tokenResponse = await _httpClient
            .AddHeader(HttpParameters.ContentType, HttpParameterValues.Form)
            .PostFormAsync<TokenResponse>(
                (HttpParameters.ClientId, _sensitiveData.ClientInfo.ClientId),
                (HttpParameters.Scope, _sensitiveData.ClientInfo.Scope),
                (HttpParameters.Code, code),
                (HttpParameters.GrantType, HttpParameterValues.AuthorizationCode),
                (HttpParameters.ClientSecret, _sensitiveData.ClientInfo.ClientSecret));

        return tokenResponse.AccessToken;
    }

    private string GetCode()
    {
        _authService.Authorize();

        return _authService.GetCode();
    }
}
