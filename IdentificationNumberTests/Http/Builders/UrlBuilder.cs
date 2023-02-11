using Flurl;
using PINTests.Http.Constants;

namespace PINTests.Http.Builders;

internal sealed class UrlBuilder
{
    private string _url;

    public UrlBuilder(string baseUrl) => _url = baseUrl;

    public void AddParams(params (string name, string value)[] parameters)
    {
        _url += UrlSeparators.ResourceSeparator;

        foreach (var param in parameters)
        {
            _url += param.name
                + UrlSeparators.AssignmentSeparator
                + param.value
                + UrlSeparators.ParamsSeparator;
        }
    }

    public string Url => _url;

    public Url FlurlUrl => new(_url);
}
