using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Subscriptions.Helpers;
using PagSeguro.DotNet.Sdk.Subscriptions.Interfaces;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Providers
{
    /// <inheritdoc cref="ICouponProvider" />
    public class CouponProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
        : SubscriptionBaseProvider(settings, flurlClient),
        ICouponProvider
    {
        /// <inheritdoc />
        public async Task<CouponResponse> CreateAsync(CouponRequest couponRequest)
        {
            return await IdempotentRequest()
                .AppendPathSegment(SubscriptionEndpoints.Coupons)
                .PostJsonAsync(couponRequest)
                .ReceiveJson<CouponResponse>();
        }

        /// <inheritdoc />
        public async Task<CouponResponse> GetByIdAsync(string couponId)
        {
            return await AuthorizedRequest()
                .AppendPathSegments(SubscriptionEndpoints.Coupons, couponId)
                .GetJsonAsync<CouponResponse>();
        }

        /// <inheritdoc />
        public async Task<CouponListResponse> ListAsync(int? offset = null, int? limit = null)
        {
            return await AuthorizedRequest()
                .AppendPathSegment(SubscriptionEndpoints.Coupons)
                .SetQueryParam("offset", offset)
                .SetQueryParam("limit", limit)
                .GetJsonAsync<CouponListResponse>();
        }

        /// <inheritdoc />
        public async Task ActivateAsync(string couponId)
        {
            await IdempotentRequest()
                .AppendPathSegments(SubscriptionEndpoints.Coupons, couponId, SubscriptionEndpoints.Activate)
                .PutAsync();
        }

        /// <inheritdoc />
        public async Task InactivateAsync(string couponId)
        {
            await IdempotentRequest()
                .AppendPathSegments(SubscriptionEndpoints.Coupons, couponId, SubscriptionEndpoints.Inactivate)
                .PutAsync();
        }
    }
}
