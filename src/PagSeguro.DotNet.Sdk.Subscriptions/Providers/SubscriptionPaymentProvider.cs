using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Subscriptions.Helpers;
using PagSeguro.DotNet.Sdk.Subscriptions.Interfaces;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Providers
{
    /// <inheritdoc cref="ISubscriptionPaymentProvider" />
    public class SubscriptionPaymentProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
        : SubscriptionBaseProvider(settings, flurlClient),
        ISubscriptionPaymentProvider
    {
        /// <inheritdoc />
        public async Task<SubscriptionPaymentResponse> GetByIdAsync(string paymentId)
        {
            return await AuthorizedRequest()
                .AppendPathSegments(SubscriptionEndpoints.Payments, paymentId)
                .GetJsonAsync<SubscriptionPaymentResponse>();
        }

        /// <inheritdoc />
        public async Task<SubscriptionPaymentListResponse> ListAsync(int? offset = null, int? limit = null)
        {
            return await AuthorizedRequest()
                .AppendPathSegment(SubscriptionEndpoints.Payments)
                .SetQueryParam("offset", offset)
                .SetQueryParam("limit", limit)
                .GetJsonAsync<SubscriptionPaymentListResponse>();
        }

        /// <inheritdoc />
        public async Task<RefundResponse> RefundAsync(string paymentId, RefundRequest refundRequest)
        {
            return await IdempotentRequest()
                .AppendPathSegments(
                    SubscriptionEndpoints.Payments,
                    paymentId,
                    SubscriptionEndpoints.RefundsSegment)
                .PostJsonAsync(refundRequest)
                .ReceiveJson<RefundResponse>();
        }

        /// <inheritdoc />
        public async Task<RefundListResponse> ListRefundsAsync(string paymentId)
        {
            return await AuthorizedRequest()
                .AppendPathSegments(
                    SubscriptionEndpoints.Payments,
                    paymentId,
                    SubscriptionEndpoints.RefundsSegment)
                .GetJsonAsync<RefundListResponse>();
        }

        /// <inheritdoc />
        public async Task<RefundResponse> GetRefundByIdAsync(string refundId)
        {
            return await AuthorizedRequest()
                .AppendPathSegments(SubscriptionEndpoints.Refunds, refundId)
                .GetJsonAsync<RefundResponse>();
        }

        /// <inheritdoc />
        public async Task<RefundListResponse> ListAllRefundsAsync(int? offset = null, int? limit = null)
        {
            return await AuthorizedRequest()
                .AppendPathSegment(SubscriptionEndpoints.Refunds)
                .SetQueryParam("offset", offset)
                .SetQueryParam("limit", limit)
                .GetJsonAsync<RefundListResponse>();
        }
    }
}
