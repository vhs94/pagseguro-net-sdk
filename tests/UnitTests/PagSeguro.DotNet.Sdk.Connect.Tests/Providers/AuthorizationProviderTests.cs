using AutoFixture;
using FluentAssertions;
using Flurl;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Validations;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Connect.Helpers;
using PagSeguro.DotNet.Sdk.Connect.Interfaces;
using PagSeguro.DotNet.Sdk.Connect.Models.Requests;
using PagSeguro.DotNet.Sdk.Connect.Models.Responses;
using PagSeguro.DotNet.Sdk.Connect.Providers;

namespace PagSeguro.DotNet.Sdk.Connect.Tests.Providers
{
    public class AuthorizationProviderTests : BaseProviderTests<AuthorizationProvider>
    {
        private ICryptoService _cryptoServiceMock = null!;

        protected override AuthorizationProvider CreateProvider()
        {
            return new AuthorizationProvider(
                _cryptoServiceMock,
                Settings,
                FlurlClientMock);
        }

        protected override void CreateMocks()
        {
            _cryptoServiceMock = Substitute.For<ICryptoService>();
        }

        [Fact]
        public async Task CreateAccessTokenByCodeAsync_PayloadIsValid_HttpRequestIsCreated()
        {
            AuthorizationCodeResponse authorizationCodeResponse = CreateAuthorizationCodeResponse();
            HttpTestMock
                .ForCallsTo(Url.Combine(Provider.BaseUrl, ConnectEndpoints.Token))
                .RespondWithJson(authorizationCodeResponse);
            AuthorizationCodeRequest authorizationCode = CreateAuthorizationCodeRequest();

            AuthorizationCodeResponse result = await Provider.CreateAccessTokenByCodeAsync(authorizationCode);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, ConnectEndpoints.Token))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(CommonHeaders.ClientId, Settings.ClientId)
                .WithHeader(CommonHeaders.ClientSecret, Settings.ClientSecret)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(new
                {
                    grant_type = ApiGrants.AuthorizationCode,
                    code = authorizationCode.Code,
                    redirect_uri = authorizationCode.RedirectUri,
                    scope = authorizationCode.Scope.ToStringApiScopes()
                })
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(authorizationCodeResponse);
        }

        [Fact]
        public async Task RefreshAccessTokenAsync_PayloadIsValid_HttpRequestIsCreated()
        {
            AuthorizationCodeResponse authorizationCodeResponse = CreateAuthorizationCodeResponse();
            HttpTestMock
                .ForCallsTo(Url.Combine(Provider.BaseUrl, ConnectEndpoints.Refresh))
                .RespondWithJson(authorizationCodeResponse);
            RefreshTokenRequest refreshTokenRequest = CreateRefreshTokenRequest();

            AuthorizationCodeResponse result = await Provider.RefreshAccessTokenAsync(refreshTokenRequest);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, ConnectEndpoints.Refresh))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(CommonHeaders.ClientId, Settings.ClientId)
                .WithHeader(CommonHeaders.ClientSecret, Settings.ClientSecret)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(new
                {
                    grant_type = ApiGrants.RefreshToken,
                    refresh_token = refreshTokenRequest.RefreshToken
                })
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(authorizationCodeResponse);
        }

        [Fact]
        public async Task RefreshAccessTokenAsync_ClientApplicationIsNotSet_ThrowsValidationException()
        {
            Settings.ClientId = null;
            RefreshTokenRequest refreshTokenRequest = CreateRefreshTokenRequest();

            Func<Task> task = async () => await Provider.RefreshAccessTokenAsync(refreshTokenRequest);

            await task.Should().ThrowAsync<MissingClientApplicationException>();
        }

        [Fact]
        public async Task RevokeTokenAsync_PayloadIsValid_HttpRequestIsCreated()
        {
            HttpTestMock
                .ForCallsTo(Url.Combine(Provider.BaseUrl, ConnectEndpoints.Revoke))
                .RespondWithJson(new { });
            RevokeTokenRequest revokeTokenRequest = CreateRevokeTokenRequest();

            await Provider.RevokeTokenAsync(revokeTokenRequest);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, ConnectEndpoints.Revoke))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(CommonHeaders.ClientId, Settings.ClientId)
                .WithHeader(CommonHeaders.ClientSecret, Settings.ClientSecret)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(new
                {
                    token = revokeTokenRequest.Token,
                    token_type_hint = revokeTokenRequest.TokenTypeHint.ToDescription()
                })
                .Times(1);
        }

        [Fact]
        public async Task RevokeTokenAsync_ClientApplicationIsNotSet_ThrowsValidationException()
        {
            Settings.ClientId = null;
            RevokeTokenRequest revokeTokenRequest = CreateRevokeTokenRequest();

            Func<Task> task = async () => await Provider.RevokeTokenAsync(revokeTokenRequest);

            await task.Should().ThrowAsync<MissingClientApplicationException>();
        }

        private RefreshTokenRequest CreateRefreshTokenRequest()
        {
            return Fixture.Create<RefreshTokenRequest>();
        }

        private RevokeTokenRequest CreateRevokeTokenRequest()
        {
            return Fixture.Build<RevokeTokenRequest>()
                .With(rt => rt.TokenTypeHint, TokenTypeHint.RefreshToken)
                .Create();
        }

        private AuthorizationCodeResponse CreateAuthorizationCodeResponse()
        {
            return Fixture.Create<AuthorizationCodeResponse>();
        }

        private AuthorizationCodeRequest CreateAuthorizationCodeRequest()
        {
            return Fixture.Build<AuthorizationCodeRequest>()
                .With(ac => ac.Scope, ApiScopes.ReadAccounts)
                .Create();
        }

        [Fact]
        public async Task CreateAccessTokenByCodeAsync_ClientApplicationIsEmpty_MissingClientApplicationExceptionIsThrown()
        {
            Settings.ClientId = null;
            Settings.ClientSecret = null;

            Func<Task> task = async () => await Provider.CreateAccessTokenByCodeAsync(null!);

            await task
                .Should()
                .ThrowAsync<MissingClientApplicationException>();
        }

        [Fact]
        public async Task CreateAccessTokenByChallengeAsync_PayloadIsValid_HttpRequestIsCreated()
        {
            ChallengeResponse challengeResponse = CreateChallengeResponse();
            HttpTestMock
                .ForCallsTo(Url.Combine(Provider.BaseUrl, ConnectEndpoints.Token))
                .RespondWithJson(challengeResponse);
            _cryptoServiceMock
                .Decrypt(challengeResponse.Challenge!)
                .Returns(challengeResponse.DecryptedChallenge);

            ChallengeResponse result = await Provider.CreateAccessTokenByChallengeAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, ConnectEndpoints.Token))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(CommonHeaders.ClientId, Settings.ClientId)
                .WithHeader(CommonHeaders.ClientSecret, Settings.ClientSecret)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(new
                {
                    grant_type = ApiGrants.Challenge,
                    scope = ApiScopes.CreateCertificate.ToDescription()
                })
                .Times(1);
            _cryptoServiceMock
                .Received(1)
                .Decrypt(result.Challenge!);
            result
                .Should()
                .BeEquivalentTo(challengeResponse);
        }

        private ChallengeResponse CreateChallengeResponse()
        {
            return Fixture
                .Build<ChallengeResponse>()
                .With(c => c.Challenge)
                .With(c => c.DecryptedChallenge)
                .Create();
        }

        [Fact]
        public async Task CreateChallengeAsync_SettingsPrivateKeyIsEmpty_ChallengeIsNotDecrypted()
        {
            Settings.PrivateKey = null;

            await Provider.CreateAccessTokenByChallengeAsync();

            _cryptoServiceMock
                .DidNotReceive()
                .Decrypt(Arg.Any<string>());
        }

        [Fact]
        public async Task CreateAccessTokenByChallengeAsync_ClientApplicationIsEmpty_MissingClientApplicationExceptionIsThrown()
        {
            Settings.ClientId = null;
            Settings.ClientSecret = null;

            Func<Task> task = Provider.CreateAccessTokenByChallengeAsync;

            await task
                .Should()
                .ThrowAsync<MissingClientApplicationException>();
        }
    }
}
