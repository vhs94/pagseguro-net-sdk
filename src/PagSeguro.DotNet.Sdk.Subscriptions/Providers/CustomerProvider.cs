using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Subscriptions.Helpers;
using PagSeguro.DotNet.Sdk.Subscriptions.Interfaces;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Providers
{
    /// <inheritdoc cref="ICustomerProvider" />
    public class CustomerProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
        : SubscriptionBaseProvider(settings, flurlClient),
        ICustomerProvider
    {
        /// <inheritdoc />
        public async Task<CustomerResponse> CreateAsync(CustomerRequest customerRequest)
        {
            return await IdempotentRequest()
                .AppendPathSegment(SubscriptionEndpoints.Customers)
                .PostJsonAsync(customerRequest)
                .ReceiveJson<CustomerResponse>();
        }

        /// <inheritdoc />
        public async Task<CustomerResponse> GetByIdAsync(string customerId)
        {
            return await AuthorizedRequest()
                .AppendPathSegments(SubscriptionEndpoints.Customers, customerId)
                .GetJsonAsync<CustomerResponse>();
        }

        /// <inheritdoc />
        public async Task<CustomerListResponse> ListAsync(int? offset = null, int? limit = null)
        {
            return await AuthorizedRequest()
                .AppendPathSegment(SubscriptionEndpoints.Customers)
                .SetQueryParam("offset", offset)
                .SetQueryParam("limit", limit)
                .GetJsonAsync<CustomerListResponse>();
        }

        /// <inheritdoc />
        public async Task<CustomerResponse> UpdateAsync(
            string customerId,
            CustomerUpdateRequest customerUpdateRequest)
        {
            return await IdempotentRequest()
                .AppendPathSegments(SubscriptionEndpoints.Customers, customerId)
                .PutJsonAsync(customerUpdateRequest)
                .ReceiveJson<CustomerResponse>();
        }

        /// <inheritdoc />
        public async Task<CustomerResponse> UpdateBillingInfoAsync(
            string customerId,
            BillingInfoRequest billingInfoRequest)
        {
            return await IdempotentRequest()
                .AppendPathSegments(SubscriptionEndpoints.Customers, customerId, "billing_info")
                .PutJsonAsync(billingInfoRequest)
                .ReceiveJson<CustomerResponse>();
        }
    }
}
