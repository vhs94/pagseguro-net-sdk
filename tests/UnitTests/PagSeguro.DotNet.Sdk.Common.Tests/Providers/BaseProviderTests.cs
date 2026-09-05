using AutoFixture;
using FluentAssertions;
using Flurl;
using Flurl.Http;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Validations;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;

namespace PagSeguro.DotNet.Sdk.Common.Tests.Providers
{
    public abstract class BaseProviderTests<TProvider> : BaseTests
        where TProvider : BaseProvider
    {
        public PagSeguroSettings Settings { get; private set; } = null!;
        public TProvider Provider { get; private set; } = default!;
        protected IServiceProvider ServiceProviderMock { get; private set; } = null!;
        protected IFlurlClient FlurlClientMock { get; private set; } = null!;

        protected override void InitializeMocks()
        {
            CreateMocks();
            ServiceProviderMock = CreateServiceProvider();
            FlurlClientMock = CreateFlurlClient();
            Settings = CreateSettings();
            Provider = CreateProvider();
            SetupMocks();
        }

        private IServiceProvider CreateServiceProvider()
            => Substitute.For<IServiceProvider>();
        private IFlurlClient CreateFlurlClient()
            => new FlurlClient();
        private PagSeguroSettings CreateSettings()
        {
            return Fixture.Build<PagSeguroSettings>()
                .With(ps => ps.Environment, PagSeguroEnvironment.Sandbox)
                .With(ps => ps.AccessToken)
                .With(ps => ps.ClientId)
                .With(ps => ps.ClientSecret)
                .With(ps => ps.PrivateKey)
                .Create();
        }

        /// <summary>
        /// Repassa a URL base para as classes de teste dos outros assemblies:
        /// BaseProvider.BaseUrl e protegida e so este assembly tem acesso interno.
        /// </summary>
        protected Url ProviderBaseUrl => Provider.BaseUrl;

        protected abstract TProvider CreateProvider();

        [Fact]
        public virtual void BaseUrl_EnvironmentIsSandbox_SandboxUrlIsAssigned()
        {
            Provider.BaseUrl.ToString()
                .Should()
                .Be(CommonEndpoints.SandboxBaseUrl);
        }

        [Fact]
        public virtual void BaseUrl_EnvironmentIsProduction_ProductionUrlIsAssigned()
        {
            Settings.Environment = PagSeguroEnvironment.Production;

            Provider.BaseUrl.ToString()
                .Should()
                .Be(CommonEndpoints.ProductionBaseUrl);
        }

        [Fact]
        public async Task EnsureAccessToken_AccessTokenIsNull_ClientNotConnectedExceptionIsThrown()
        {
            Settings.AccessToken = null;

            var act = () => Provider.EnsureAccessToken();

            act.Should().Throw<ClientNotConnectedException>();
        }

        [Fact]
        public async Task EnsureChallenge_ChallengeIsNull_ClientNotConnectedWithChallengeExceptionIsThrown()
        {
            Settings.Challenge = null;

            var act = () => Provider.EnsureChallenge();

            act.Should().Throw<ClientNotConnectedWithChallengeException>();
        }

        [Fact]
        public async Task EnsureClientApplication_ClientIdIsNull_MissingClientApplicationExceptionIsThrown()
        {
            Settings.ClientId = null;

            var act = () => Provider.EnsureClientApplication();

            act.Should().Throw<MissingClientApplicationException>();
        }

        [Fact]
        public async Task EnsureClientApplication_ClientSecretIsNull_MissingClientApplicationExceptionIsThrown()
        {
            Settings.ClientSecret = null;

            var act = () => Provider.EnsureClientApplication();

            act.Should().Throw<MissingClientApplicationException>();
        }

        [Fact]
        public async Task EnsurePrivateKey_PrivateKeyIsNull_PrivateKeyNotFoundExceptionIsThrown()
        {
            Settings.PrivateKey = null;

            var act = () => Provider.EnsurePrivateKey();

            act.Should().Throw<PrivateKeyNotFoundException>();
        }
    }
}
