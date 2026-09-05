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
    public class SubscriptionPreferenceProviderTests : BaseProviderTests<SubscriptionPreferenceProvider>
    {
        protected override SubscriptionPreferenceProvider CreateProvider()
        {
            return new SubscriptionPreferenceProvider(Settings, FlurlClientMock);
        }

        protected override void CreateMocks()
        {
        }

        // A API de Assinaturas roda em um host proprio.
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
        public async Task GetRetryPreferencesAsync_PreferencesExist_HttpRequestIsCreated()
        {
            RetryPreferenceResponse retryPreferenceResponse = Fixture.Create<RetryPreferenceResponse>();
            HttpTestMock
                .ForCallsTo(Url.Combine(ProviderBaseUrl, SubscriptionEndpoints.RetryPreferences))
                .RespondWithJson(retryPreferenceResponse);

            RetryPreferenceResponse result = await Provider.GetRetryPreferencesAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(ProviderBaseUrl, SubscriptionEndpoints.RetryPreferences))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Get)
                .Times(1);
            result.Should().BeEquivalentTo(retryPreferenceResponse);
        }

        [Fact]
        public async Task UpdateRetryPreferencesAsync_PayloadIsValid_HttpRequestIsCreated()
        {
            HttpTestMock
                .ForCallsTo(Url.Combine(ProviderBaseUrl, SubscriptionEndpoints.RetryPreferences))
                .RespondWith(status: 200);
            RetryPreferenceRequest retryPreferenceRequest = CreateRetryPreferenceRequest();

            await Provider.UpdateRetryPreferencesAsync(retryPreferenceRequest);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(ProviderBaseUrl, SubscriptionEndpoints.RetryPreferences))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(SubscriptionHeaders.IdempotencyKey)
                .WithVerb(HttpMethod.Put)
                .WithRequestJson(retryPreferenceRequest)
                .Times(1);
        }

        [Fact]
        public async Task UpdateRetryPreferencesAsync_TriesAreSerializedWithTheApiFieldNames()
        {
            // A API usa first_try/second_try/third_try/finally. Sem os
            // [JsonPropertyName] o camelCase mandaria firstTry e a chamada seria
            // aceita com 200 sem alterar nada.
            HttpTestMock
                .ForCallsTo(Url.Combine(ProviderBaseUrl, SubscriptionEndpoints.RetryPreferences))
                .RespondWith(status: 200);

            await Provider.UpdateRetryPreferencesAsync(CreateRetryPreferenceRequest());

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(ProviderBaseUrl, SubscriptionEndpoints.RetryPreferences))
                .With(call => call.RequestBody.Contains("first_try")
                    && call.RequestBody.Contains("second_try")
                    && call.RequestBody.Contains("third_try")
                    && call.RequestBody.Contains("\"finally\""))
                .Times(1);
        }

        [Fact]
        public async Task CreatePublicKeyAsync_Always_HttpRequestIsCreatedWithPutVerb()
        {
            // A criacao da chave publica das assinaturas e PUT, nao POST: um POST
            // devolve 405 na API.
            SubscriptionPublicKeyResponse publicKeyResponse =
                Fixture.Create<SubscriptionPublicKeyResponse>();
            HttpTestMock
                .ForCallsTo(Url.Combine(ProviderBaseUrl, SubscriptionEndpoints.PublicKeys))
                .RespondWithJson(publicKeyResponse);

            SubscriptionPublicKeyResponse result = await Provider.CreatePublicKeyAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(ProviderBaseUrl, SubscriptionEndpoints.PublicKeys))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(SubscriptionHeaders.IdempotencyKey)
                .WithVerb(HttpMethod.Put)
                .Times(1);
            result.Should().BeEquivalentTo(publicKeyResponse);
        }

        private static RetryPreferenceRequest CreateRetryPreferenceRequest()
        {
            return new RetryPreferenceRequest
            {
                FirstTry = 1,
                SecondTry = 3,
                ThirdTry = 5,
                Finally = "CANCEL"
            };
        }
    }
}
