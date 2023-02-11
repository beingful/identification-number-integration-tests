using PINTests.Http.Clients;
using PINTests.Http.Constants;
using PINTests.Integration.IdNumberVerifier.Models;
using PINTests.Integration.Token;

namespace PINTests.Integration.IdNumberVerifier;

internal sealed class IdNumberVerifierService
{
    private readonly TokenService _tokenService;
    private readonly FlurlHttpClient _httpClient;

    public IdNumberVerifierService(TokenService tokenService)
    {
        _tokenService = tokenService;
        _httpClient = new(baseUrl: Endpoints.CheckId);
    }

    public async Task<int> CheckIdAsync(string number)
    {
        IdNumberVerifierRequest request = new() { Number = number };

        string token = await _tokenService.GetTokenAsync();

        IdNumberVerifierResponse response = await _httpClient
            .AddHeader(HttpParameters.Accept, HttpParameterValues.Json)
            .AddHeader(HttpParameters.ContentType, HttpParameterValues.Json)
            .AddOAuthBearerToken(token)
            .PostWithContentAsync<IdNumberVerifierResponse>(request);

        return response.Id;
    }
}
