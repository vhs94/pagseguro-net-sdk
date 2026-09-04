using AutoFixture;
using FluentAssertions;
using PagSeguro.DotNet.Sdk.Connect.Models.Requests;
using PagSeguro.DotNet.Sdk.Connect.Models.Responses;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers
{
    public class ApplicationIntegrationTests : BaseIntegrationTests
    {
        [Fact]
        public async Task CreateAsync_RequestIsValid_ApplicationIsCreated()
        {
            ApplicationRequest applicationRequest = CreateApplicationRequest();

            ApplicationResponse result = await Client
                .ForApplication()
                .CreateAsync(applicationRequest);

            ApplicationResponse applicationResponse = await Client
                .ForApplication()
                .GetByClientIdAsync(result.ClientId!);
            result
                .Should()
                .NotBeNull();
            result
                .Should()
                .BeEquivalentTo(applicationRequest);
            result
                .Should()
                .BeEquivalentTo(applicationResponse, options => options.Excluding(app => app.ClientSecret));

        }

        private ApplicationRequest CreateApplicationRequest()
        {
            string validUrl = "http://myurl.com";
            return Fixture.Build<ApplicationRequest>()
                .With(app => app.RedirectUrl, validUrl)
                .With(app => app.Site, validUrl)
                .Create();
        }
    }
}
