using AutoFixture;
using FluentAssertions;
using Flurl;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Connect.Helpers;
using PagSeguro.DotNet.Sdk.Connect.Models.Requests;
using PagSeguro.DotNet.Sdk.Connect.Models.Responses;
using PagSeguro.DotNet.Sdk.Connect.Providers;

namespace PagSeguro.DotNet.Sdk.Connect.Tests.Providers
{
    public class ApplicationProviderTests : BaseProviderTests<ApplicationProvider>
    {
        private ApplicationResponse _applicationResponse = null!;

        protected override ApplicationProvider CreateProvider()
        {
            return new ApplicationProvider(Settings, FlurlClientMock);
        }

        protected override void SetupMocks()
        {
            _applicationResponse = CreateApplicationResponse();
            HttpTestMock
                .ForCallsTo(
                    Url.Combine(Provider.BaseUrl, ConnectEndpoints.Application),
                    Url.Combine(Provider.BaseUrl, ConnectEndpoints.Application, "*"))
                .RespondWithJson(_applicationResponse);
        }

        private ApplicationResponse CreateApplicationResponse()
        {
            return Fixture.Create<ApplicationResponse>();
        }

        [Fact]
        public async Task CreateAsync_ApplicationIsValid_HttpRequestIsCreated()
        {
            ApplicationRequest application = CreateApplication();

            ApplicationResponse result = await Provider.CreateAsync(application);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, ConnectEndpoints.Application))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(application)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(_applicationResponse);
        }

        private ApplicationRequest CreateApplication()
        {
            return Fixture.Create<ApplicationRequest>();
        }

        [Fact]
        public async Task GetByClientIdAsync_ApplicationIdIsValid_HttpRequestIsCreated()
        {
            string applicationId = "appId";

            ApplicationResponse result = await Provider.GetByClientIdAsync(applicationId);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, ConnectEndpoints.Application, applicationId))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Get)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(_applicationResponse);
        }
    }
}
