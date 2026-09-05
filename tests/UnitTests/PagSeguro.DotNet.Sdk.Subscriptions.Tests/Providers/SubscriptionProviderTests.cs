using AutoFixture;
using FluentAssertions;
using Flurl;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Subscriptions.Helpers;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;
using PagSeguro.DotNet.Sdk.Subscriptions.Providers;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Tests.Providers
{
    public class SubscriptionProviderTests : BaseProviderTests<SubscriptionProvider>
    {
        protected override SubscriptionProvider CreateProvider()
        {
            return new SubscriptionProvider(Settings, FlurlClientMock);
        }

        protected override void CreateMocks()
        {
        }

        // A API de Assinaturas roda em um host proprio, entao os testes de URL base
        // herdados de BaseProviderTests sao sobrescritos aqui.
        [Fact]
        public override void BaseUrl_EnvironmentIsSandbox_SandboxUrlIsAssigned()
        {
            Provider.BaseUrl.ToString().Should().Be(SubscriptionEndpoints.SandboxBaseUrl);
        }

        [Fact]
        public override void BaseUrl_EnvironmentIsProduction_ProductionUrlIsAssigned()
        {
            Settings.Environment = PagSeguroEnvironment.Production;

            Provider.BaseUrl.ToString().Should().Be(SubscriptionEndpoints.ProductionBaseUrl);
        }

        [Fact]
        public async Task CreateAsync_SubscriptionIsValid_HttpRequestIsCreated()
        {
            SubscriptionResponse response = Fixture.Create<SubscriptionResponse>();
            HttpTestMock
                .ForCallsTo(Url.Combine(Provider.BaseUrl, SubscriptionEndpoints.Subscriptions))
                .RespondWithJson(response);
            SubscriptionRequest request = new()
            {
                ReferenceId = "sub-1",
                Plan = new PlanReference { Id = "PLAN_1" },
                Customer = new CustomerReference { Id = "CUST_1" }
            };

            SubscriptionResponse result = await Provider.CreateAsync(request);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, SubscriptionEndpoints.Subscriptions))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(SubscriptionHeaders.IdempotencyKey)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(request)
                .Times(1);
            result.Should().BeEquivalentTo(response);
        }

        [Theory]
        [InlineData("suspend")]
        [InlineData("activate")]
        [InlineData("cancel")]
        [InlineData("retry")]
        public async Task LifecycleActions_SubscriptionExists_PutIsSentToTheAction(string action)
        {
            string subscriptionId = "SUBS_" + Guid.NewGuid();
            string url = Url.Combine(
                Provider.BaseUrl, SubscriptionEndpoints.Subscriptions, subscriptionId, action);
            HttpTestMock.ForCallsTo(url).RespondWith(status: 204);

            Task call = action switch
            {
                "suspend" => Provider.SuspendAsync(subscriptionId),
                "activate" => Provider.ActivateAsync(subscriptionId),
                "cancel" => Provider.CancelAsync(subscriptionId),
                _ => Provider.RetryAsync(subscriptionId)
            };
            await call;

            HttpTestMock
                .ShouldHaveCalled(url)
                .WithVerb(HttpMethod.Put)
                .WithOAuthBearerToken(Settings.Token)
                .Times(1);
        }

        [Fact]
        public async Task ListInvoicesAsync_SubscriptionExists_InvoicesUrlIsCalled()
        {
            string subscriptionId = "SUBS_" + Guid.NewGuid();
            string url = Url.Combine(
                Provider.BaseUrl, SubscriptionEndpoints.Subscriptions, subscriptionId, "invoices");
            HttpTestMock.ForCallsTo(url).RespondWithJson(Fixture.Create<InvoiceListResponse>());

            await Provider.ListInvoicesAsync(subscriptionId);

            HttpTestMock
                .ShouldHaveCalled(url)
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }
    }
}
