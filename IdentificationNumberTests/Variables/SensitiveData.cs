using IdentificationNumberTests.Variables.Models;
using Microsoft.Extensions.Configuration;

namespace IntegrationTests.Variables;

internal sealed class SensitiveData
{
    public SensitiveData(IConfiguration configuration)
    {
        ClientInfo = Get<ClientInfo>(configuration, nameof(ClientInfo));
        OrganizationInfo = Get<OrganizationInfo>(configuration, nameof(OrganizationInfo));
        AccountInfo = Get<AccountInfo>(configuration, nameof(AccountInfo));
        CosmosDbInfo = Get<CosmosDbInfo>(configuration, nameof(CosmosDbInfo));
    }

    public ClientInfo ClientInfo { get; }

    public OrganizationInfo OrganizationInfo { get; }

    public AccountInfo AccountInfo { get; }

    public CosmosDbInfo CosmosDbInfo { get; }

    private T Get<T>(IConfiguration configuration, string section)
        where T : class
    {
        return configuration.GetSection(section).Get<T>()!;
    }
}
