using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PINTests.Selenium.Constants;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace PINTests.Selenium.Providers;

internal static class WebDriverProvider
{
    public static WebDriver GetWebDriver()
    {
        ConfigureDriver();

        ChromeOptions chromOptions = CreateChromOptions();

        return CreateWebDriver(chromOptions);
    }

    private static void ConfigureDriver()
    {
        new DriverManager()
            .SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
    }

    private static ChromeOptions CreateChromOptions()
    {
        ChromeOptions chromeOptions = new();

        chromeOptions.AddArgument(BrowserSettings.Headless);
        chromeOptions.AddArgument(BrowserSettings.UiLanguage);
        chromeOptions.AddArgument(BrowserSettings.GuestAccount);

        return chromeOptions;
    }

    private static ChromeDriver CreateWebDriver(ChromeOptions options)
    {
        ChromeDriver webDriver = new(options);

        webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        return webDriver;
    }
}
