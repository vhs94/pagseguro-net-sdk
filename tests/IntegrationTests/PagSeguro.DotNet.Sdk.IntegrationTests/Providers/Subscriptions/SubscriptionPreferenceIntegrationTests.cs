using FluentAssertions;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers.Subscriptions
{
    /// <summary>
    /// Cobertura viva de GET/PUT /preferences/retries e de PUT /public-keys.
    /// </summary>
    public class SubscriptionPreferenceIntegrationTests : BaseIntegrationTests
    {
        [Fact]
        public async Task GetRetryPreferencesAsync_Always_PreferencesAreReturned()
        {
            RetryPreferenceResponse result = await Client
                .ForSubscriptionPreference()
                .GetRetryPreferencesAsync();

            result.Should().NotBeNull();
            result.FirstTry.Should().BeOneOf(1, 3, 5, 7);
            result.SecondTry.Should().BeOneOf(1, 3, 5, 7);
            result.ThirdTry.Should().BeOneOf(1, 3, 5, 7);
            result.Finally.Should().BeOneOf("SUSPEND", "CANCEL");
        }

        [Fact]
        public async Task UpdateRetryPreferencesAsync_PreferencesAreValid_ChangeIsPersisted()
        {
            RetryPreferenceResponse original = await Client
                .ForSubscriptionPreference()
                .GetRetryPreferencesAsync();

            try
            {
                // Escolhe valores diferentes dos atuais para que a leitura de volta
                // realmente comprove que o PUT teve efeito.
                int newThirdTry = original.ThirdTry == 7 ? 5 : 7;
                string newFinally = original.Finally == "CANCEL" ? "SUSPEND" : "CANCEL";

                await Client
                    .ForSubscriptionPreference()
                    .UpdateRetryPreferencesAsync(new RetryPreferenceRequest
                    {
                        FirstTry = 1,
                        SecondTry = 3,
                        ThirdTry = newThirdTry,
                        Finally = newFinally
                    });

                RetryPreferenceResponse updated = await Client
                    .ForSubscriptionPreference()
                    .GetRetryPreferencesAsync();

                updated.FirstTry.Should().Be(1);
                updated.SecondTry.Should().Be(3);
                updated.ThirdTry.Should().Be(newThirdTry);
                updated.Finally.Should().Be(newFinally);
            }
            finally
            {
                // A preferencia e do vendedor inteiro: restaura para nao afetar as
                // outras execucoes da suite.
                await Client
                    .ForSubscriptionPreference()
                    .UpdateRetryPreferencesAsync(new RetryPreferenceRequest
                    {
                        FirstTry = original.FirstTry,
                        SecondTry = original.SecondTry,
                        ThirdTry = original.ThirdTry,
                        Finally = original.Finally
                    });
            }
        }

        [Fact]
        public async Task CreatePublicKeyAsync_Always_PublicKeyIsReturned()
        {
            SubscriptionPublicKeyResponse result = await Client
                .ForSubscriptionPreference()
                .CreatePublicKeyAsync();

            result.Should().NotBeNull();
            result.PublicKey.Should().NotBeNullOrWhiteSpace();

            // A chave criada passa a ser a vigente.
            SubscriptionPublicKeyResponse current = await Client
                .ForSubscriptionPreference()
                .GetPublicKeyAsync();

            current.PublicKey.Should().Be(result.PublicKey);
        }
    }
}
