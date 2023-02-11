using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Newtonsoft.Json;

namespace PINTests.Http.Clients;

internal sealed class FlurlHttpClient
{
    private FlurlRequest _request;

    public FlurlHttpClient(Url baseUrl, JsonSerializerSettings? serializerSettings = default)
        : this(baseUrl)
    {
        ConfigureHttpClient(serializerSettings);
    }

    public FlurlHttpClient(Url baseUrl)
    {
        _request = new(baseUrl);
    }

    public FlurlHttpClient AddHeader(string name, string value)
    {
        _request = _request.WithHeader(name, value);

        return this;
    }

    public FlurlHttpClient AddOAuthBearerToken(string token)
    {
        _request = _request.WithOAuthBearerToken(token);

        return this;
    }

    public async Task<T> PostWithContentAsync<T>(object request)
        where T : class
    {
        IFlurlResponse response = await _request.PostJsonAsync(request);

        return await response.GetJsonAsync<T>();
    }

    public async Task<T> PostFormAsync<T>(params (string name, string value)[] parameters)
        where T : class
    {
        IFlurlResponse response = await _request
            .PostMultipartAsync(content =>
            {
                Array.ForEach(parameters, param =>
                {
                    content.AddString(param.name, param.value);
                });
            });

        return await response.GetJsonAsync<T>();
    }

    private void ConfigureHttpClient(JsonSerializerSettings? serializerSettings)
    {
        FlurlHttp.Configure(settings =>
        {
            settings.JsonSerializer = new NewtonsoftJsonSerializer(serializerSettings);
        });
    }
}
