using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Subscriptions.Helpers;
using PagSeguro.DotNet.Sdk.Subscriptions.Interfaces;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Providers
{
    /// <inheritdoc cref="ISubscriptionPreferenceProvider" />
    public class SubscriptionPreferenceProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
        : SubscriptionBaseProvider(settings, flurlClient),
        ISubscriptionPreferenceProvider
    {
        private const string NotificationPreferences = "/preferences/notifications";

        /// <inheritdoc />
        public async Task<NotificationPreferenceResponse> GetNotificationPreferencesAsync()
        {
            return await AuthorizedRequest()
                .AppendPathSegment(NotificationPreferences)
                .GetJsonAsync<NotificationPreferenceResponse>();
        }

        /// <inheritdoc />
        public async Task<NotificationPreferenceResponse> UpdateNotificationPreferencesAsync(
            NotificationPreferenceRequest notificationPreferenceRequest)
        {
            return await IdempotentRequest()
                .AppendPathSegment(NotificationPreferences)
                .PutJsonAsync(notificationPreferenceRequest)
                .ReceiveJson<NotificationPreferenceResponse>();
        }

        /// <inheritdoc />
        public async Task<SubscriptionPublicKeyResponse> GetPublicKeyAsync()
        {
            return await AuthorizedRequest()
                .AppendPathSegment(SubscriptionEndpoints.PublicKeys)
                .GetJsonAsync<SubscriptionPublicKeyResponse>();
        }

        /// <inheritdoc />
        public async Task<SubscriptionPublicKeyResponse> CreatePublicKeyAsync()
        {
            return await IdempotentRequest()
                .AppendPathSegment(SubscriptionEndpoints.PublicKeys)
                .PutAsync()
                .ReceiveJson<SubscriptionPublicKeyResponse>();
        }

        /// <inheritdoc />
        public async Task<RetryPreferenceResponse> GetRetryPreferencesAsync()
        {
            return await AuthorizedRequest()
                .AppendPathSegment(SubscriptionEndpoints.RetryPreferences)
                .GetJsonAsync<RetryPreferenceResponse>();
        }

        /// <inheritdoc />
        public async Task UpdateRetryPreferencesAsync(RetryPreferenceRequest retryPreferenceRequest)
        {
            await IdempotentRequest()
                .AppendPathSegment(SubscriptionEndpoints.RetryPreferences)
                .PutJsonAsync(retryPreferenceRequest);
        }
    }
}
