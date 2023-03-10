using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using IntegrationTests.Http.Constants;
using IntegrationTests.Selenium.Constants;
using System.Text.RegularExpressions;

namespace IntegrationTests.Selenium.Clients;

internal sealed class WebDriverClient
{
    private readonly WebDriver _webDriver;

    public WebDriverClient(WebDriver webDriver) =>
        _webDriver = webDriver;

    public void GoTo(string url) =>
        _webDriver.Navigate().GoToUrl(url);

    public void ClickOnElementWithAttribute(string attributeName, string attributeValue)
    {
        IWebElement element = FindElementByAttribute(attributeName, attributeValue);

        element.Click();
    }

    public void InsertTextInInputString(string attributeName, string attributeText, string text)
    {
        IWebElement element = FindElementByAttribute(attributeName, attributeText, WebElements.Input);

        element.SendKeys(text);
    }

    public string GetUrlParameterValueByName(string name)
    {
        WebDriverWait webDriverWait = new(_webDriver, TimeSpan.FromSeconds(7));

        webDriverWait.Until((webDriver) => webDriver.Url.Contains(name));

        string url = _webDriver.Url;

        Regex parameterPattern = new($"{name}{UrlSeparators.AssignmentSeparator}\\S+{UrlSeparators.ParamsSeparator}");
        Match match = parameterPattern.Match(url);

        return match.Value
            .Replace($"{name}{UrlSeparators.AssignmentSeparator}", string.Empty)
            .Replace(UrlSeparators.ParamsSeparator, string.Empty);
    }

    private IWebElement FindElementByAttribute(string attributeName, string attributeValue, string elementTag = "*") =>
        FindElementBy(By.XPath($"//{elementTag}[@{attributeName}='{attributeValue}']"));

    private IWebElement FindElementBy(By by) =>
        _webDriver.FindElement(by);
}
