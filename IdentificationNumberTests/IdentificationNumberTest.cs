using FluentAssertions;
using IdentificationNumberTests.Api;
using IdentificationNumberTests.Database;
using IntegrationTests.Database.Models;
using IntegrationTests.Database.Repositories;
using IntegrationTests.Fixtures;
using Swagger.Model;
using Xunit;

namespace IntegrationTests;

public class IdentificationNumberTest : IClassFixture<ServicesFixture>
{
    private readonly IdNumberVerifierService _idNumberVerifierService;
    private readonly UserInfoDbRepository _userInfoDbRepository;

    public IdentificationNumberTest(ServicesFixture serviceFixture) 
    {
        _idNumberVerifierService = serviceFixture.GetService<IdNumberVerifierService>()!;
        _userInfoDbRepository = serviceFixture.GetService<UserInfoDbRepository>()!;
    }

    public static IEnumerable<object[]> TextpayerIdNumberAndDateOfBirth =>
        new List<object[]> { new object[] { "3270313912", "1989-07-15" } };

    [Theory]
    [MemberData(nameof(TextpayerIdNumberAndDateOfBirth))]
    public async void PostCheckId_WithValidData_ShouldReturnOK(string taxpayerIdNumber, string birthDate)
    {
        // Arrange
        NumberRequest numberRequest = new(taxpayerIdNumber);

        UserInfoContext expectedResult = new()
        {
            AccountNumber = taxpayerIdNumber,
            BirthDate = birthDate
        };

        // Act
        long? id = await _idNumberVerifierService.CheckIdAsync(numberRequest);

        UserInfoContext? userInfo = await Try.ExecuteAsync(ct =>
        {
            return _userInfoDbRepository.GetUserInfoAsync($"{id}");
        }, userInfo => userInfo != null);

        expectedResult.Id = $"{id}";

        // Assert
        id.Should().BePositive();
        userInfo.Should().BeEquivalentTo(expectedResult);
    }
}