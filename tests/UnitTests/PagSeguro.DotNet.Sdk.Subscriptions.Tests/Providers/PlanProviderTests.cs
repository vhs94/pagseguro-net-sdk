using AutoFixture;
using FluentAssertions;
using Flurl;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Subscriptions.Helpers;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;
using PagSeguro.DotNet.Sdk.Subscriptions.Providers;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Tests.Providers
{
    public class PlanProviderTests : BaseProviderTests<PlanProvider>
    {
        protected override PlanProvider CreateProvider()
        {
            return new PlanProvider(Settings, FlurlClientMock);
        }

        protected override void CreateMocks()
        {
        }

        // A API de Assinaturas roda em um host proprio, entao os testes de URL base
        // herdados de BaseProviderTests sao sobrescritos aqui.
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
        public async Task CreateAsync_PlanIsValid_HttpRequestIsCreated()
        {
            PlanResponse planResponse = Fixture.Create<PlanResponse>();
            HttpTestMock
                .ForCallsTo(Url.Combine(ProviderBaseUrl, SubscriptionEndpoints.Plans))
                .RespondWithJson(planResponse);
            PlanRequest planRequest = CreatePlanRequest();

            PlanResponse result = await Provider.CreateAsync(planRequest);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(ProviderBaseUrl, SubscriptionEndpoints.Plans))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(SubscriptionHeaders.IdempotencyKey)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(planRequest)
                .Times(1);
            result.Should().BeEquivalentTo(planResponse);
        }

        [Fact]
        public async Task CreateAsync_SetupFeeIsNotInformed_SetupFeeIsOmitted()
        {
            // Regressao: a API recusa setup_fee: 0 com "must contain only digits
            // greater than 0", o que quebrava a criacao de qualquer plano sem adesao.
            HttpTestMock
                .ForCallsTo(Url.Combine(ProviderBaseUrl, SubscriptionEndpoints.Plans))
                .RespondWithJson(Fixture.Create<PlanResponse>());

            await Provider.CreateAsync(CreatePlanRequest());

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(ProviderBaseUrl, SubscriptionEndpoints.Plans))
                .With(call => !call.RequestBody.Contains("setup_fee"))
                .Times(1);
        }

        [Fact]
        public async Task GetByIdAsync_PlanExists_HttpRequestIsCreated()
        {
            string planId = "PLAN_" + Guid.NewGuid();
            HttpTestMock
                .ForCallsTo(Url.Combine(ProviderBaseUrl, SubscriptionEndpoints.Plans, planId))
                .RespondWithJson(Fixture.Create<PlanResponse>());

            await Provider.GetByIdAsync(planId);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(ProviderBaseUrl, SubscriptionEndpoints.Plans, planId))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [Fact]
        public async Task ListAsync_PagingIsInformed_QueryParamsAreSent()
        {
            HttpTestMock
                .ForCallsTo(Url.Combine(ProviderBaseUrl, SubscriptionEndpoints.Plans))
                .RespondWithJson(Fixture.Create<PlanListResponse>());

            await Provider.ListAsync(offset: 20, limit: 10);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(ProviderBaseUrl, SubscriptionEndpoints.Plans))
                .WithQueryParam("offset", 20)
                .WithQueryParam("limit", 10)
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [Fact]
        public async Task ListAsync_PagingIsNotInformed_QueryParamsAreOmitted()
        {
            HttpTestMock
                .ForCallsTo(Url.Combine(ProviderBaseUrl, SubscriptionEndpoints.Plans))
                .RespondWithJson(Fixture.Create<PlanListResponse>());

            await Provider.ListAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(ProviderBaseUrl, SubscriptionEndpoints.Plans))
                .WithoutQueryParam("offset")
                .WithoutQueryParam("limit")
                .Times(1);
        }

        [Fact]
        public async Task ActivateAsync_PlanExists_PutIsSentToActivate()
        {
            string planId = "PLAN_" + Guid.NewGuid();
            string url = Url.Combine(
                ProviderBaseUrl, SubscriptionEndpoints.Plans, planId, SubscriptionEndpoints.Activate);
            HttpTestMock.ForCallsTo(url).RespondWith(status: 200);

            await Provider.ActivateAsync(planId);

            // A API usa PUT nas acoes de ciclo de vida; POST devolve 405.
            HttpTestMock
                .ShouldHaveCalled(url)
                .WithVerb(HttpMethod.Put)
                .Times(1);
        }

        [Fact]
        public async Task InactivateAsync_PlanExists_PutIsSentToInactivate()
        {
            string planId = "PLAN_" + Guid.NewGuid();
            string url = Url.Combine(
                ProviderBaseUrl, SubscriptionEndpoints.Plans, planId, SubscriptionEndpoints.Inactivate);
            HttpTestMock.ForCallsTo(url).RespondWith(status: 200);

            await Provider.InactivateAsync(planId);

            HttpTestMock
                .ShouldHaveCalled(url)
                .WithVerb(HttpMethod.Put)
                .Times(1);
        }

        private static PlanRequest CreatePlanRequest()
        {
            return new PlanRequest
            {
                Name = "Plano SDK",
                Amount = new Models.Shared.Money { Value = 1990, Currency = "BRL" },
                Interval = new Models.Shared.PlanInterval { Unit = "MONTH", Length = 1 }
            };
        }
    }
}
