using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Subscriptions.Helpers;
using PagSeguro.DotNet.Sdk.Subscriptions.Interfaces;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Providers
{
    /// <inheritdoc cref="ISubscriptionProvider" />
    public class SubscriptionProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
        : SubscriptionBaseProvider(settings, flurlClient),
        ISubscriptionProvider
    {
        /// <inheritdoc />
        public async Task<SubscriptionResponse> CreateAsync(SubscriptionRequest subscriptionRequest)
        {
            return await IdempotentRequest()
                .AppendPathSegment(SubscriptionEndpoints.Subscriptions)
                .PostJsonAsync(subscriptionRequest)
                .ReceiveJson<SubscriptionResponse>();
        }

        /// <inheritdoc />
        public async Task<SubscriptionResponse> GetByIdAsync(string subscriptionId)
        {
            return await AuthorizedRequest()
                .AppendPathSegments(SubscriptionEndpoints.Subscriptions, subscriptionId)
                .GetJsonAsync<SubscriptionResponse>();
        }

        /// <inheritdoc />
        public async Task<SubscriptionListResponse> ListAsync(int? offset = null, int? limit = null)
        {
            return await AuthorizedRequest()
                .AppendPathSegment(SubscriptionEndpoints.Subscriptions)
                .SetQueryParam("offset", offset)
                .SetQueryParam("limit", limit)
                .GetJsonAsync<SubscriptionListResponse>();
        }

        /// <inheritdoc />
        public async Task<InvoiceListResponse> ListInvoicesAsync(
            string subscriptionId,
            int? offset = null,
            int? limit = null)
        {
            return await AuthorizedRequest()
                .AppendPathSegments(SubscriptionEndpoints.Subscriptions, subscriptionId, "invoices")
                .SetQueryParam("offset", offset)
                .SetQueryParam("limit", limit)
                .GetJsonAsync<InvoiceListResponse>();
        }

        /// <inheritdoc />
        public async Task<SubscriptionResponse> UpdateAsync(
            string subscriptionId,
            SubscriptionUpdateRequest subscriptionUpdateRequest)
        {
            return await IdempotentRequest()
                .AppendPathSegments(SubscriptionEndpoints.Subscriptions, subscriptionId)
                .PutJsonAsync(subscriptionUpdateRequest)
                .ReceiveJson<SubscriptionResponse>();
        }

        /// <inheritdoc />
        public async Task SuspendAsync(string subscriptionId)
        {
            await IdempotentRequest()
                .AppendPathSegments(SubscriptionEndpoints.Subscriptions, subscriptionId, SubscriptionEndpoints.Suspend)
                .PutAsync();
        }

        /// <inheritdoc />
        public async Task ActivateAsync(string subscriptionId)
        {
            await IdempotentRequest()
                .AppendPathSegments(SubscriptionEndpoints.Subscriptions, subscriptionId, SubscriptionEndpoints.Activate)
                .PutAsync();
        }

        /// <inheritdoc />
        public async Task CancelAsync(string subscriptionId)
        {
            await IdempotentRequest()
                .AppendPathSegments(SubscriptionEndpoints.Subscriptions, subscriptionId, SubscriptionEndpoints.Cancel)
                .PutAsync();
        }

        /// <inheritdoc />
        public async Task RetryAsync(string subscriptionId)
        {
            await IdempotentRequest()
                .AppendPathSegments(SubscriptionEndpoints.Subscriptions, subscriptionId, "retry")
                .PutAsync();
        }
    }
}
