using AutoFixture;
using FluentAssertions;
using Flurl;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Subscriptions.Helpers;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;
using PagSeguro.DotNet.Sdk.Subscriptions.Providers;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Tests.Providers
{
    public class SubscriptionPaymentProviderTests : BaseProviderTests<SubscriptionPaymentProvider>
    {
        private const string RefundId = "REFU_5F1F4C4C-1F2E-4C0B-9E4A-9E1A1B2C3D4E";

        protected override SubscriptionPaymentProvider CreateProvider()
        {
            return new SubscriptionPaymentProvider(Settings, FlurlClientMock);
        }

        protected override void CreateMocks()
        {
        }

        [Fact]
        public override void BaseUrl_EnvironmentIsSandbox_SandboxUrlIsAssigned()
        {
            ProviderBaseUrl.ToString().Should().Be(SubscriptionEndpoints.SandboxBaseUrl);
        }

        [Fact]
        public override void BaseUrl_EnvironmentIsProduction_ProductionUrlIsAssigned()
        {
            Settings.Environment = PagSeguroEnvironment.Production;

            ProviderBaseUrl.ToString().Should().Be(SubscriptionEndpoints.ProductionBaseUrl);
        }

        [Fact]
        public async Task GetRefundByIdAsync_RefundExists_HttpRequestIsCreated()
        {
            RefundResponse refundResponse = Fixture.Create<RefundResponse>();
            string url = Url.Combine(ProviderBaseUrl, SubscriptionEndpoints.Refunds, RefundId);
            HttpTestMock.ForCallsTo(url).RespondWithJson(refundResponse);

            RefundResponse result = await Provider.GetRefundByIdAsync(RefundId);

            HttpTestMock
                .ShouldHaveCalled(url)
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Get)
                .Times(1);
            result.Should().BeEquivalentTo(refundResponse);
        }

        [Fact]
        public async Task GetRefundByIdAsync_ResponseHasPaymentAndType_FieldsAreDeserialized()
        {
            string url = Url.Combine(ProviderBaseUrl, SubscriptionEndpoints.Refunds, RefundId);
            HttpTestMock.ForCallsTo(url).RespondWith(
                """
                {
                  "id": "REFU_1",
                  "payment": { "id": "PAYM_1", "amount": { "value": 1990, "currency": "BRL" } },
                  "amount": { "value": 1990, "currency": "BRL" },
                  "status": "SUCCESS",
                  "type": "FULL"
                }
                """);

            RefundResponse result = await Provider.GetRefundByIdAsync(RefundId);

            result.Payment!.Id.Should().Be("PAYM_1");
            result.Payment.Amount!.Value.Should().Be(1990);
            result.Type.Should().Be("FULL");
            result.Status.Should().Be("SUCCESS");
        }
    }
}
