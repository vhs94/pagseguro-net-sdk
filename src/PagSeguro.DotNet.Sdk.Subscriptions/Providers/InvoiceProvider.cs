using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Subscriptions.Helpers;
using PagSeguro.DotNet.Sdk.Subscriptions.Interfaces;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Providers
{
    /// <inheritdoc cref="IInvoiceProvider" />
    public class InvoiceProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
        : SubscriptionBaseProvider(settings, flurlClient),
        IInvoiceProvider
    {
        /// <inheritdoc />
        public async Task<InvoiceResponse> GetByIdAsync(string invoiceId)
        {
            return await AuthorizedRequest()
                .AppendPathSegments(SubscriptionEndpoints.Invoices, invoiceId)
                .GetJsonAsync<InvoiceResponse>();
        }

        /// <inheritdoc />
        public async Task<SubscriptionPaymentListResponse> ListPaymentsAsync(string invoiceId)
        {
            return await AuthorizedRequest()
                .AppendPathSegments(SubscriptionEndpoints.Invoices, invoiceId, "payments")
                .GetJsonAsync<SubscriptionPaymentListResponse>();
        }
    }
}
