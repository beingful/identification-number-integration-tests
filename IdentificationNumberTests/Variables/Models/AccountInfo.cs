#nullable disable

namespace IdentificationNumberTests.Variables.Models;

internal sealed class AccountInfo
{
    public string Email { get; init; }

    public string Password { get; init; }

    public string OtpSecretKey { get; init; }
}
