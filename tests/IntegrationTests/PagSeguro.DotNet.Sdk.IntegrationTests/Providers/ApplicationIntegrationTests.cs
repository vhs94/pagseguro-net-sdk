using AutoFixture;
using FluentAssertions;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Application;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers
{
    public class ApplicationIntegrationTests : BaseIntegrationTests
    {
        [Fact]
        public async Task CreateAsync_RequestIsValid_ApplicationIsCreated()
        {
            ApplicationWriteDto applicationWriteDto = CreateApplicationWriteDto();

            ApplicationReadDto result = await Client
                .ForApplication()
                .CreateAsync(applicationWriteDto);

            ApplicationReadDto applicationReadDto = await Client
                .ForApplication()
                .GetByClientIdAsync(result.ClientId);
            result
                .Should()
                .NotBeNull();
            result
                .Should()
                .BeEquivalentTo(applicationWriteDto);
            result
                .Should()
                .BeEquivalentTo(applicationReadDto, options => options.Excluding(app => app.ClientSecret));

        }

        private ApplicationWriteDto CreateApplicationWriteDto()
        {
            string validUrl = "http://myurl.com";
            return Fixture.Build<ApplicationWriteDto>()
                .With(app => app.RedirectUrl, validUrl)
                .With(app => app.Site, validUrl)
                .Create();
        }
    }
}
