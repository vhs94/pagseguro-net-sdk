using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Subscriptions.Helpers;
using PagSeguro.DotNet.Sdk.Subscriptions.Interfaces;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Providers
{
    /// <inheritdoc cref="IPlanProvider" />
    public class PlanProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
        : SubscriptionBaseProvider(settings, flurlClient),
        IPlanProvider
    {
        /// <inheritdoc />
        public async Task<PlanResponse> CreateAsync(PlanRequest planRequest)
        {
            return await IdempotentRequest()
                .AppendPathSegment(SubscriptionEndpoints.Plans)
                .PostJsonAsync(planRequest)
                .ReceiveJson<PlanResponse>();
        }

        /// <inheritdoc />
        public async Task<PlanResponse> GetByIdAsync(string planId)
        {
            return await AuthorizedRequest()
                .AppendPathSegments(SubscriptionEndpoints.Plans, planId)
                .GetJsonAsync<PlanResponse>();
        }

        /// <inheritdoc />
        public async Task<PlanListResponse> ListAsync(int? offset = null, int? limit = null)
        {
            return await AuthorizedRequest()
                .AppendPathSegment(SubscriptionEndpoints.Plans)
                .SetQueryParam("offset", offset)
                .SetQueryParam("limit", limit)
                .GetJsonAsync<PlanListResponse>();
        }

        /// <inheritdoc />
        public async Task<PlanResponse> UpdateAsync(string planId, PlanRequest planRequest)
        {
            return await IdempotentRequest()
                .AppendPathSegments(SubscriptionEndpoints.Plans, planId)
                .PutJsonAsync(planRequest)
                .ReceiveJson<PlanResponse>();
        }

        /// <inheritdoc />
        public async Task ActivateAsync(string planId)
        {
            await IdempotentRequest()
                .AppendPathSegments(SubscriptionEndpoints.Plans, planId, SubscriptionEndpoints.Activate)
                .PutAsync();
        }

        /// <inheritdoc />
        public async Task InactivateAsync(string planId)
        {
            await IdempotentRequest()
                .AppendPathSegments(SubscriptionEndpoints.Plans, planId, SubscriptionEndpoints.Inactivate)
                .PutAsync();
        }
    }
}
