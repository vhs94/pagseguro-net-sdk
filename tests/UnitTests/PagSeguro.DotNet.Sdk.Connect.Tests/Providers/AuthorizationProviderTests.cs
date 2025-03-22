using AutoFixture;
using FluentAssertions;
using Flurl;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Validations;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.AuthorizationCode;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.Challenge;
using PagSeguro.DotNet.Sdk.Connect.Helpers;
using PagSeguro.DotNet.Sdk.Connect.Interfaces;
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
                Settings);
        }

        protected override void CreateMocks()
        {
            _cryptoServiceMock = Substitute.For<ICryptoService>();
        }

        [Fact]
        public async Task CreateAccessTokenByCodeAsync_PayloadIsValid_HttpRequestIsCreated()
        {
            AuthorizationCodeReadDto authorizationCodeReadDto = CreateAuthorizationCodeReadDto();
            HttpTestMock
                .ForCallsTo(Url.Combine(Provider.BaseUrl, ConnectEndpoints.Token))
                .RespondWithJson(authorizationCodeReadDto);
            AuthorizationCodeWriteDto authorizationCode = CreateAuthorizationCodeWriteDto();

            AuthorizationCodeReadDto result = await Provider.CreateAccessTokenByCodeAsync(authorizationCode);

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
                .BeEquivalentTo(authorizationCodeReadDto);
        }

        private AuthorizationCodeReadDto CreateAuthorizationCodeReadDto()
        {
            return Fixture.Create<AuthorizationCodeReadDto>();
        }

        private AuthorizationCodeWriteDto CreateAuthorizationCodeWriteDto()
        {
            return Fixture.Build<AuthorizationCodeWriteDto>()
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
            ChallengeReadDto challengeReadDto = CreateChallengeReadDto();
            HttpTestMock
                .ForCallsTo(Url.Combine(Provider.BaseUrl, ConnectEndpoints.Token))
                .RespondWithJson(challengeReadDto);
            _cryptoServiceMock
                .Decrypt(challengeReadDto.Challenge!)
                .Returns(challengeReadDto.DecryptedChallenge);

            ChallengeReadDto result = await Provider.CreateAccessTokenByChallengeAsync();

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
                .BeEquivalentTo(challengeReadDto);
        }

        private ChallengeReadDto CreateChallengeReadDto()
        {
            return Fixture
                .Build<ChallengeReadDto>()
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
