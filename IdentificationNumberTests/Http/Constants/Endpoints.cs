namespace IntegrationTests.Http.Constants;

internal static class Endpoints
{
    public static string CheckId
        => "https://idnumberverifier.azurewebsites.net/api/checkid?code=qGCwfHsVy0jkMCS8NNvnAU0w7-76JVYMyxGFMuSkOpoUAzFuEsq3fQ==";

    public static string AuthorizationEndpoint(string tenantId)
        => BaseOauth2Path(tenantId) + "authorize";

    public static string TokenEndpoint(string tenantId)
        => BaseOauth2Path(tenantId) + "token";

    private static string BaseOauth2Path(string tenantId)
        => $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/";
}
