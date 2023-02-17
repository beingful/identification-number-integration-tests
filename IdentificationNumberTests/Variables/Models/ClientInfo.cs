#nullable disable

namespace IdentificationNumberTests.Variables.Models;

internal sealed class ClientInfo
{
    public string ClientId { get; init; }

    public string ClientSecret { get; init; }

    public string Scope { get; init; }
}
