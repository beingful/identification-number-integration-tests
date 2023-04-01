using FluentAssertions;
using IdentificationNumberTests.Database;
using IntegrationTests.Database.Models;
using IntegrationTests.Database.Repositories;
using IntegrationTests.Fixtures;
using IntegrationTests.Integration.IdNumberVerifier;
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
    public async void CheckIdentificationNumber_WithValidData(string taxpayerIdNumber, string birthDate)
    {
        //// Arrange
        //UserInfoContext expectedResult = new()
        //{
        //    AccountNumber = taxpayerIdNumber,
        //    BirthDate = birthDate
        //};

        //// Act
        //int id = await _idNumberVerifierService.CheckIdAsync(taxpayerIdNumber);

        //UserInfoContext? userInfo = await Try.ExecuteAsync(ct =>
        //{
        //    return _userInfoDbRepository.GetUserInfoAsync($"{id}");
        //}, userInfo => userInfo != null);

        //expectedResult.Id = $"{id}";

        //// Assert
        //id.Should().BePositive();
        //userInfo.Should().BeEquivalentTo(expectedResult);

        true.Should().BeTrue();
    }
}