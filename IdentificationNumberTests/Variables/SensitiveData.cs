using Microsoft.Extensions.Configuration;

namespace PINTests.Variables;

internal sealed class SensitiveData
{
    private readonly IConfiguration _configuration;

    public SensitiveData(IConfiguration configuration) =>
        _configuration = configuration;

    public string ClientId =>
        Get(ClientInfo, nameof(ClientId));

    public string ClientSecret =>
        Get(ClientInfo, nameof(ClientSecret));

    public string Scope =>
        Get(ClientInfo, nameof(Scope));

    public string TenantId =>
        Get(OrganizationInfo, nameof(TenantId));

    public string Email =>
        Get(AccountInfo, nameof(Email));

    public string Password =>
        Get(AccountInfo, nameof(Password));

    public string OtpSecretKey =>
        Get(AccountInfo, nameof(OtpSecretKey));

    public string TaxpayerIdentificationNumber =>
        Get(PersonalInfo, nameof(TaxpayerIdentificationNumber));

    public string BirthDate =>
        Get(PersonalInfo, nameof(BirthDate));

    public string AccountEndpoint =>
        Get(CosmosDbInfo, nameof(AccountEndpoint));

    public string AccountKey =>
        Get(CosmosDbInfo, nameof(AccountKey));

    public string DbName =>
        Get(CosmosDbInfo, nameof(DbName));

    private string ClientInfo =>
        nameof(ClientInfo);

    private string OrganizationInfo =>
        nameof(OrganizationInfo);

    private string AccountInfo =>
        nameof(AccountInfo);

    private string PersonalInfo =>
        nameof(PersonalInfo);

    private string CosmosDbInfo =>
        nameof(CosmosDbInfo);

    private string Get(string sectionName, string variableName) =>
        _configuration.GetSection(sectionName).GetSection(variableName).Value!;
}
