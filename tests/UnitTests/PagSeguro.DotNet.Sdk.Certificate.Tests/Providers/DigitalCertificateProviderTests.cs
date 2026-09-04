using AutoFixture;
using FluentAssertions;
using Flurl;
using PagSeguro.DotNet.Sdk.Certificate.Helpers;
using PagSeguro.DotNet.Sdk.Certificate.Models.Responses;
using PagSeguro.DotNet.Sdk.Certificate.Providers;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Validations;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;

namespace PagSeguro.DotNet.Sdk.Certificate.Tests.Providers
{
    public class DigitalCertificateProviderTests : BaseProviderTests<DigitalCertificateProvider>
    {
        private CertificateResponse _certificateResponse = null!;

        protected override DigitalCertificateProvider CreateProvider()
        {
            return new DigitalCertificateProvider(Settings, FlurlClientMock);
        }

        protected override void SetupMocks()
        {
            _certificateResponse = CreateCertificateResponse();
            HttpTestMock
                .ForCallsTo(Url.Combine(Provider.BaseUrl, CertificateEndpoints.Certificate))
                .WithVerb(HttpMethod.Post)
                .RespondWithJson(_certificateResponse);
        }

        private CertificateResponse CreateCertificateResponse()
        {
            return Fixture.Create<CertificateResponse>();
        }

        [Fact]
        public async Task CreateAsync_CertificateIsValid_HttpRequestIsCreated()
        {
            CertificateResponse result = await Provider.CreateAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, CertificateEndpoints.Certificate))
                .WithOAuthBearerToken(Settings.AccessToken)
                .WithHeader(CertificateHeaders.Challenge, Settings.Challenge)
                .WithVerb(HttpMethod.Post)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(_certificateResponse);
        }

        [Fact]
        public async Task CreateAsync_ChallengeIsEmpty_ClientNotConnectedWithChallengeExceptionIsThrown()
        {
            Settings.Challenge = null;

            Func<Task> task = Provider.CreateAsync;

            await task
                .Should()
                .ThrowAsync<ClientNotConnectedWithChallengeException>();
        }
    }
}
