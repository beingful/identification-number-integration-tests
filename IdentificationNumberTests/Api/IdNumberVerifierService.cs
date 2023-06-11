using IntegrationTests.Http.Clients;
using IntegrationTests.Http.Constants;
using IntegrationTests.Integration.Token;
using Swagger.Model;

namespace IdentificationNumberTests.Api;

internal sealed class IdNumberVerifierService
{
    private readonly TokenService _tokenService;
    private readonly FlurlHttpClient _httpClient;

    public IdNumberVerifierService(TokenService tokenService)
    {
        _tokenService = tokenService;
        _httpClient = new(baseUrl: Endpoints.CheckId);
    }

    public async Task<long?> CheckIdAsync(NumberRequest request)
    {
        string token = await _tokenService.GetTokenAsync();

        NumberResponse response = await _httpClient
            .AddHeader(HttpParameters.Accept, HttpParameterValues.Json)
            .AddHeader(HttpParameters.ContentType, HttpParameterValues.Json)
            .AddOAuthBearerToken(token)
            .PostWithContentAsync<NumberResponse>(request);

        return response.Id;
    }
}
