using AutoFixture;
using FluentAssertions;
using Flurl;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Models.Shared;
using PagSeguro.DotNet.Sdk.Orders.Providers.Splits;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Splits
{
    public class SplitProviderTests : BaseProviderTests<SplitProvider>
    {
        private const string SplitId = "SPLI_2B5A9C5C-4C5E-4B7A-9C1D-8E7F6A5B4C3D";

        protected override SplitProvider CreateProvider()
        {
            return new SplitProvider(Settings, FlurlClientMock);
        }

        protected override void CreateMocks()
        {
        }

        [Fact]
        public async Task GetByIdAsync_SplitExists_HttpRequestIsCreated()
        {
            SplitResponse splitResponse = Fixture.Create<SplitResponse>();
            string url = Url.Combine(ProviderBaseUrl, OrderEndpoint.Splits, SplitId);
            HttpTestMock.ForCallsTo(url).RespondWithJson(splitResponse);

            SplitResponse result = await Provider.GetByIdAsync(SplitId);

            HttpTestMock
                .ShouldHaveCalled(url)
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Get)
                .Times(1);
            result.Should().BeEquivalentTo(splitResponse);
        }

        [Fact]
        public async Task GetByIdAsync_ResponseIsReturned_ReceiversAreDeserialized()
        {
            string url = Url.Combine(ProviderBaseUrl, OrderEndpoint.Splits, SplitId);
            HttpTestMock.ForCallsTo(url).RespondWith(
                """
                {
                  "id": "SPLI_1",
                  "method": "FIXED",
                  "receivers": [
                    { "payment": { "id": "PAY_1" }, "account": { "id": "ACCO_1" },
                      "amount": { "value": 4000 }, "type": "PRIMARY" },
                    { "payment": { "id": "PAY_2" }, "account": { "id": "ACCO_2" },
                      "amount": { "value": 1000 }, "type": "SECONDARY" }
                  ],
                  "links": [ { "rel": "SELF", "href": "https://x", "media": "application/json", "type": "GET" } ]
                }
                """);

            SplitResponse result = await Provider.GetByIdAsync(SplitId);

            result.Method.Should().Be(SplitMethod.Fixed);
            result.Receivers.Should().HaveCount(2);
            result.Receivers.First().Payment!.Id.Should().Be("PAY_1");
            result.Receivers.First().Account!.Id.Should().Be("ACCO_1");
            result.Receivers.First().Amount!.Value.Should().Be(4000);
            result.Receivers.Last().Type.Should().Be("SECONDARY");
            result.Links.Should().ContainSingle();
        }

        [Fact]
        public async Task ReleaseCustodyAsync_PayloadIsValid_HttpRequestIsCreated()
        {
            string url = Url.Combine(
                ProviderBaseUrl, OrderEndpoint.Splits, SplitId, OrderEndpoint.CustodyRelease);
            HttpTestMock.ForCallsTo(url).RespondWith(status: 200);
            SplitCustodyReleaseRequest releaseRequest = new()
            {
                Receivers =
                [
                    new SplitCustodyReceiverRequest
                    {
                        Account = new SplitAccount { Id = "ACCO_1" }
                    }
                ]
            };

            await Provider.ReleaseCustodyAsync(SplitId, releaseRequest);

            HttpTestMock
                .ShouldHaveCalled(url)
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(releaseRequest)
                .Times(1);
        }
    }
}
