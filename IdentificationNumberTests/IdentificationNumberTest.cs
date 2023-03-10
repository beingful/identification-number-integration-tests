using FluentAssertions;
using IdentificationNumberTests.Database;
using IntegrationTests.Database.Models;
using IntegrationTests.Database.Repositories;
using IntegrationTests.Fixtures;
using IntegrationTests.Integration.IdNumberVerifier;
using IntegrationTests.Variables;
using Xunit;

namespace IntegrationTests;

public class IdentificationNumberTest : IClassFixture<ServicesFixture>
{
    private readonly IdNumberVerifierService _idNumberVerifierService;
    private readonly UserInfoDbRepository _userInfoDbRepository;
    private readonly SensitiveData _sensitiveData;

    public IdentificationNumberTest(ServicesFixture serviceFixture) 
    {
        _idNumberVerifierService = serviceFixture.GetService<IdNumberVerifierService>()!;
        _userInfoDbRepository = serviceFixture.GetService<UserInfoDbRepository>()!;
        _sensitiveData = serviceFixture.GetService<SensitiveData>()!;
    }

    [Fact]
    public async void CheckIdentificationNumber_WithValidData()
    {
        // Act
        int id = await _idNumberVerifierService
            .CheckIdAsync(_sensitiveData.PersonalInfo.TaxpayerIdNumber);

        UserInfoContext? userInfo = await Try.ExecuteAsync(ct =>
        {
            return _userInfoDbRepository.GetUserInfoAsync($"{id}");
        }, userInfo => userInfo != null);

        UserInfoContext expectedResult = new()
        {
            Id = $"{id}",
            AccountNumber = _sensitiveData.PersonalInfo.TaxpayerIdNumber,
            BirthDate = _sensitiveData.PersonalInfo.BirthDate
        };

        // Assert
        id.Should().BePositive();
        userInfo.Should().BeEquivalentTo(expectedResult);
    }
}