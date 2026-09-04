using AutoFixture;
using FluentAssertions;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Common.Tests;
using PagSeguro.DotNet.Sdk.Connect.Models.Requests;
using PagSeguro.DotNet.Sdk.Connect.Models.Responses;
using PagSeguro.DotNet.Sdk.Settings;

namespace PagSeguro.DotNet.Sdk.Tests
{
    public class PagSeguroClientTests : BaseTests
    {
        private PagSeguroClient _client = null!;
        private ClientSettings _settings = null!;
        private AuthorizationCodeResponse _authorizationCodeResponse = null!;
        private ChallengeResponse _challengeResponse = null!;

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
            _authorizationCodeResponse = CreateAuthorizationCodeResponse();
            _challengeResponse = CreateChallengeResponse();
            _client
                .ForAuthorization()
                .CreateAccessTokenByCodeAsync(Arg.Any<AuthorizationCodeRequest>())
                .Returns(_authorizationCodeResponse);
            _client
                .ForAuthorization()
                .CreateAccessTokenByChallengeAsync()
                .Returns(_challengeResponse);
        }

        private AuthorizationCodeResponse CreateAuthorizationCodeResponse()
        {
            return Fixture.Create<AuthorizationCodeResponse>();
        }

        private ChallengeResponse CreateChallengeResponse()
        {
            return Fixture.Create<ChallengeResponse>();
        }

        [Fact]
        public async Task ConnectAsync_AuthorizationCodeIsValid_AcessTokenIsSet()
        {
            AuthorizationCodeRequest request = CreateAuthorizationCodeRequest();

            AuthorizationCodeResponse result = await _client.ConnectAsync(request);

            await _client
                .ForAuthorization()
                .Received(1)
                .CreateAccessTokenByCodeAsync(request);
            _client.Settings
                .AccessToken
                .Should().Be(_authorizationCodeResponse.AccessToken);
            result
                .Should()
                .BeEquivalentTo(_authorizationCodeResponse);
        }

        private AuthorizationCodeRequest CreateAuthorizationCodeRequest()
        {
            return Fixture.Create<AuthorizationCodeRequest>();
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
                .Should().Be(_challengeResponse.AccessToken);
            _client.Settings
                .Challenge
                .Should().Be(_challengeResponse.DecryptedChallenge);
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
