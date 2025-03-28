﻿using AutoFixture;
using FluentAssertions;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Common.Settings;

namespace PagSeguro.DotNet.Sdk.Common.Tests.Providers
{
    public abstract class BaseProviderTests<TProvider> : BaseTests
        where TProvider : IProvider
    {
        public PagSeguroSettings Settings { get; private set; } = null!;
        public TProvider Provider { get; private set; } = default!;
        protected IServiceProvider ServiceProviderMock { get; private set; } = null!;

        protected override void InitializeMocks()
        {
            CreateMocks();
            ServiceProviderMock = CreateServiceProvider();
            Settings = CreateSettings();
            Provider = CreateProvider();
            SetupMocks();
        }

        private IServiceProvider CreateServiceProvider()
            => Substitute.For<IServiceProvider>();
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
