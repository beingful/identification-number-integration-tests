#nullable disable

namespace IntegrationTests.Database.Models;

internal sealed class UserInfoContext
{
    public string Id { get; init; }

    public string BirthDate { get; init; }

    public string AccountNumber { get; init; }
}
