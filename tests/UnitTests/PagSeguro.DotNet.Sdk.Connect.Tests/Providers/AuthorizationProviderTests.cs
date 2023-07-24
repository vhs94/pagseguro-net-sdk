using AutoFixture;
using Flurl;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.Challenge;
using PagSeguro.DotNet.Sdk.Connect.Helpers;
using PagSeguro.DotNet.Sdk.Connect.Interfaces;
using PagSeguro.DotNet.Sdk.Connect.Providers;

namespace PagSeguro.DotNet.Sdk.Connect.Tests.Providers
{
    public class AuthorizationProviderTests : BaseProviderTests<AuthorizationProvider>
    {
        private ICryptoService _cryptoServiceMock;

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

        protected override void SetupMocks()
        {
            HttpTestMock
                .ForCallsTo(Url.Combine(Provider.BaseUrl, ConnectEndpoints.Token))
                .RespondWithJson(CreateChallengeReadDto());
        }

        private ChallengeReadDto CreateChallengeReadDto()
        {
            return Fixture.Create<ChallengeReadDto>();
        }

        [Fact]
        public async Task CreateAccessTokenByChallengeAsync_PayloadIsValid_HttpRequestIsCreated()
        {
            await Provider.CreateAccessTokenByChallengeAsync();

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
        }

        [Fact]
        public async Task CreateChallengeAsync_SettingsHasPrivateKey_ChallengeIsDecrypted()
        {
            ChallengeReadDto result = await Provider.CreateAccessTokenByChallengeAsync();

            _cryptoServiceMock
                .Received(1)
                .Decrypt(result.Challenge);
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
    }
}
