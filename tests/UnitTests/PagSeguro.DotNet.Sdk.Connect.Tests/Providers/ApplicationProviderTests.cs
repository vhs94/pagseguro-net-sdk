using AutoFixture;
using FluentAssertions;
using Flurl;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Application;
using PagSeguro.DotNet.Sdk.Connect.Helpers;
using PagSeguro.DotNet.Sdk.Connect.Providers;

namespace PagSeguro.DotNet.Sdk.Connect.Tests.Providers
{
    public class ApplicationProviderTests : BaseProviderTests<ApplicationProvider>
    {
        private ApplicationReadDto _applicationReadDto;

        protected override ApplicationProvider CreateProvider()
        {
            return new ApplicationProvider(Settings);
        }

        protected override void SetupMocks()
        {
            _applicationReadDto = CreateApplicationReadDto();
            HttpTestMock
                .ForCallsTo(
                    Url.Combine(Provider.BaseUrl, ConnectEndpoints.Application),
                    Url.Combine(Provider.BaseUrl, ConnectEndpoints.Application, "*"))
                .RespondWithJson(_applicationReadDto);
        }

        private ApplicationReadDto CreateApplicationReadDto()
        {
            return Fixture.Create<ApplicationReadDto>();
        }

        [Fact]
        public async Task CreateAsync_ApplicationIsValid_HttpRequestIsCreated()
        {
            ApplicationWriteDto application = CreateApplication();

            ApplicationReadDto result = await Provider.CreateAsync(application);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, ConnectEndpoints.Application))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(application)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(_applicationReadDto);
        }

        private ApplicationWriteDto CreateApplication()
        {
            return Fixture.Create<ApplicationWriteDto>();
        }

        [Fact]
        public async Task GetByClientIdAsync_ApplicationIdIsValid_HttpRequestIsCreated()
        {
            string applicationId = "appId";

            ApplicationReadDto result = await Provider.GetByClientIdAsync(applicationId);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, ConnectEndpoints.Application, applicationId))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Get)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(_applicationReadDto);
        }
    }
}
