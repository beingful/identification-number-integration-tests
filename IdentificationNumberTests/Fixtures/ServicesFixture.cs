using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using Microsoft.Extensions.Configuration;
using PINTests.Constants;
using PINTests.Database;
using Microsoft.EntityFrameworkCore;
using PINTests.Variables;
using PINTests.Selenium.Clients;
using PINTests.Selenium.Providers;
using PINTests.Integration.Authorization;
using PINTests.Integration.IdNumberVerifier;
using PINTests.Integration.Otp;
using PINTests.Integration.Token;
using PINTests.Database.Repositories;

namespace PINTests.Fixtures;

public sealed class ServicesFixture
{
    private readonly IServiceProvider _serviceProvider;

    public ServicesFixture() =>
        _serviceProvider = ConfigureServices();

    private ServiceProvider ConfigureServices() =>
        new ServiceCollection()
            .AddTransient<IConfiguration>(sp =>
            {
                return new ConfigurationBuilder()
                        .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName)
                        .AddJsonFile(FileNames.Appsettings)
                        .Build();
            })
            .AddTransient<SensitiveData>()
            .AddTransient<WebDriverClient>(sp =>
            {
                WebDriver webDriver = WebDriverProvider.GetWebDriver();

                return new WebDriverClient(webDriver);
            })
            .AddTransient<OtpService>()
            .AddTransient<AuthService>()
            .AddTransient<TokenService>()
            .AddTransient<IdNumberVerifierService>()
            .AddDbContext<UserInfoDbContext>((sp, options) =>
            {
                SensitiveData sensitiveData = sp.GetService<SensitiveData>()!;

                options.UseCosmos(
                    accountEndpoint: sensitiveData.AccountEndpoint,
                    accountKey: sensitiveData.AccountKey,
                    databaseName: sensitiveData.DbName);
            })
            .AddTransient<UserInfoDbRepository>()
            .BuildServiceProvider();

    public TService? GetService<TService>() where TService : class =>
        _serviceProvider.GetService<TService>();
}
