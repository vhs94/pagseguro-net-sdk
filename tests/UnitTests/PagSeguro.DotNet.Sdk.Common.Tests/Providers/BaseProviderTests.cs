using AutoFixture;
using FluentAssertions;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Common.Settings;

namespace PagSeguro.DotNet.Sdk.Common.Tests.Providers
{
    public abstract class BaseProviderTests<TProvider> : BaseTests
        where TProvider : IProvider
    {
        public PagSeguroSettings Settings { get; private set; }
        public TProvider Provider { get; private set; }

        protected override void InitializeMocks()
        {
            CreateMocks();
            Settings = CreateSettings();
            Provider = CreateProvider();
            SetupMocks();
        }

        private PagSeguroSettings CreateSettings()
        {
            return Fixture.Build<PagSeguroSettings>()
                .With(ps => ps.Environment, PagSeguroEnvironment.Sandbox)
                .Create();
        }

        protected abstract TProvider CreateProvider();

        [Fact]
        public void BaseUrl_EnvironmentIsSandbox_SandboxUrlIsAssigned()
        {
            Provider.BaseUrl.ToString()
                .Should()
                .Be(CommonEndpoints.SandboxBaseUrl);
        }

        [Fact]
        public void BaseUrl_EnvironmentIsProduction_ProductionUrlIsAssigned()
        {
            Settings.Environment = PagSeguroEnvironment.Production;

            Provider.BaseUrl.ToString()
                .Should()
                .Be(CommonEndpoints.ProductionBaseUrl);
        }
    }
}
