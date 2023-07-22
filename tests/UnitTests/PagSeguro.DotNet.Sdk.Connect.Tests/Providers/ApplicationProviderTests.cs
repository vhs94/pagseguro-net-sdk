using AutoFixture;
using Flurl;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Application;
using PagSeguro.DotNet.Sdk.Connect.Helpers;
using PagSeguro.DotNet.Sdk.Connect.Providers;

namespace PagSeguro.DotNet.Sdk.Connect.Tests.Providers
{
    public class ApplicationProviderTests : BaseProviderTests<ApplicationProvider>
    {
        protected override ApplicationProvider CreateProvider()
        {
            return new ApplicationProvider(Settings);
        }

        [Fact]
        public async Task CreateApplicationAsync_ApplicationIsValid_HttpRequestIsCreated()
        {
            var application = CreateApplication();

            await Provider.CreateApplicationAsync(application);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, ConnectEndpoints.Application))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(application)
                .Times(1);
        }

        private ApplicationWriteDto CreateApplication()
        {
            return Fixture.Create<ApplicationWriteDto>();
        }

        [Fact]
        public async Task GetApplicationAsync_ApplicationIdIsValid_HttpRequestIsCreated()
        {
            var applicationId = "appId";

            await Provider.GetApplicationAsync(applicationId);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, ConnectEndpoints.Application, applicationId))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }
    }
}
