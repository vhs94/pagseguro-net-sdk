using AutoFixture;
using FluentAssertions;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Common.Tests;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.AuthorizationCode;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.Challenge;
using PagSeguro.DotNet.Sdk.Settings;

namespace PagSeguro.DotNet.Sdk.Tests
{
    public class PagSeguroClientTests : BaseTests
    {
        private PagSeguroClient _client;
        private ClientSettings _settings;
        private AuthorizationCodeReadDto _authorizationCodeReadDto;
        private ChallengeReadDto _challengeReadDto;

        protected override void CreateMocks()
        {
            _settings = CreateClientSettings();
            _client = Substitute.For<PagSeguroClient>(_settings);
        }

        private ClientSettings CreateClientSettings()
        {
            return Fixture.Create<ClientSettings>();
        }

        protected override void SetupMocks()
        {
            _authorizationCodeReadDto = CreateAuthorizationCodeReadDto();
            _challengeReadDto = CreateChallengeReadDto();
            _client
                .ForAuthorization()
                .CreateAccessTokenByCodeAsync(Arg.Any<AuthorizationCodeWriteDto>())
                .Returns(_authorizationCodeReadDto);
            _client
                .ForAuthorization()
                .CreateAccessTokenByChallengeAsync()
                .Returns(_challengeReadDto);
        }

        private AuthorizationCodeReadDto CreateAuthorizationCodeReadDto()
        {
            return Fixture.Create<AuthorizationCodeReadDto>();
        }

        private ChallengeReadDto CreateChallengeReadDto()
        {
            return Fixture.Create<ChallengeReadDto>();
        }

        [Fact]
        public async Task ConnectAsync_AuthorizationCodeIsValid_AcessTokenIsSet()
        {
            AuthorizationCodeWriteDto writeDto = CreateAuthorizationCodeWriteDto();

            AuthorizationCodeReadDto result = await _client.ConnectAsync(writeDto);

            await _client
                .ForAuthorization()
                .Received(1)
                .CreateAccessTokenByCodeAsync(writeDto);
            _client.Settings
                .AccessToken
                .Should().Be(_authorizationCodeReadDto.AccessToken);
            result
                .Should()
                .BeEquivalentTo(_authorizationCodeReadDto);
        }

        private AuthorizationCodeWriteDto CreateAuthorizationCodeWriteDto()
        {
            return Fixture.Create<AuthorizationCodeWriteDto>();
        }

        [Fact]
        public async Task ConnectChallengeAsync_ChallengeIsValid_AcessTokenAndDecryptedChallengeIsSet()
        {
            await _client.ConnectChallengeAsync();

            await _client
                .ForAuthorization()
                .Received(1)
                .CreateAccessTokenByChallengeAsync();
            _client.Settings
                .AccessToken
                .Should().Be(_challengeReadDto.AccessToken);
            _client.Settings
                .Challenge
                .Should().Be(_challengeReadDto.DecryptedChallenge);
        }

        [Fact]
        public void ConfigureClientApplication_ClientIsValid_ClientApplicationIsSet()
        {
            string clientId = "id";
            string clientSecret = "secret";

            _client.ConfigureClientApplication(clientId, clientSecret);

            _client.Settings
                .ClientId
                .Should().Be(clientId);
            _client.Settings
                .ClientSecret
                .Should().Be(clientSecret);
        }
    }
}
