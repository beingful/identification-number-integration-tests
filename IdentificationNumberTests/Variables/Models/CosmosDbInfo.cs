#nullable disable

namespace IdentificationNumberTests.Variables.Models;

internal sealed class CosmosDbInfo
{
    public string AccountEndpoint { get; init; }

    public string AccountKey { get; init; }

    public string DbName { get; init; }
}
