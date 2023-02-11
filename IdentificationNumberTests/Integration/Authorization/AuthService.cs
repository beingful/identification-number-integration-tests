using PINTests.Http.Builders;
using PINTests.Http.Constants;
using PINTests.Variables;
using PINTests.Selenium.Clients;
using PINTests.Selenium.Constants;
using PINTests.Integration.Otp;

namespace PINTests.Integration.Authorization;

internal sealed class AuthService
{
    private readonly OtpService _otpService;
    private readonly WebDriverClient _webDriverClient;
    private readonly SensitiveData _sensitiveData;
    private readonly UrlBuilder _urlBuilder;

    public AuthService(OtpService otpService, WebDriverClient webDriverClient, SensitiveData variableProvider)
    {
        _otpService = otpService;
        _webDriverClient = webDriverClient;
        _sensitiveData = variableProvider;
        _urlBuilder = new(baseUrl: Endpoints.AuthorizationEndpoint(variableProvider.TenantId));
    }

    public void Authorize()
    {
        GoToAuthUrl();
        SignInMicrosoftAccount();
        CompleteSecondStepVerification();
    }

    public string GetCode() =>
        _webDriverClient.GetUrlParameterValueByName(HttpParameters.Code);

    private void GoToAuthUrl()
    {
        _urlBuilder.AddParams(
            (HttpParameters.ClientId, _sensitiveData.ClientId),
            (HttpParameters.ResponseType, HttpParameterValues.Code),
            (HttpParameters.Scope, _sensitiveData.Scope));

        _webDriverClient.GoTo(_urlBuilder.Url);
    }

    private void SignInMicrosoftAccount()
    {
        _webDriverClient.InsertTextInInputString(WebElementsAttributes.Type, AttributesValues.Email, _sensitiveData.Email);
        _webDriverClient.ClickOnElementWithAttribute(WebElementsAttributes.Value, AttributesValues.Next);

        _webDriverClient.InsertTextInInputString(WebElementsAttributes.Type, AttributesValues.Password, _sensitiveData.Password);
        _webDriverClient.ClickOnElementWithAttribute(WebElementsAttributes.Value, AttributesValues.SignIn);
    }

    private void CompleteSecondStepVerification()
    {
        string oneTimePassword = _otpService.GetOneTimePassword();

        _webDriverClient.InsertTextInInputString(WebElementsAttributes.Name, AttributesValues.OneTimeCode, oneTimePassword);
        _webDriverClient.ClickOnElementWithAttribute(WebElementsAttributes.Value, AttributesValues.Verify);
    }
}
