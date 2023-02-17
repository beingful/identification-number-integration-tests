using OtpNet;
using PINTests.Variables;

namespace PINTests.Integration.Otp;

internal sealed class OtpService
{
    private readonly SensitiveData _sensitiveData;

    public OtpService(SensitiveData variableProvider) =>
        _sensitiveData = variableProvider;

    public string GetOneTimePassword()
    {
        var otpKeyBytes = Base32Encoding.ToBytes(_sensitiveData.AccountInfo.OtpSecretKey);

        return new Totp(otpKeyBytes).ComputeTotp();
    }
}
