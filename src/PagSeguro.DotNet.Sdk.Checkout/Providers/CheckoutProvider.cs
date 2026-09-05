using Flurl.Http;
using PagSeguro.DotNet.Sdk.Checkout.Helpers;
using PagSeguro.DotNet.Sdk.Checkout.Interfaces;
using PagSeguro.DotNet.Sdk.Checkout.Models.Requests;
using PagSeguro.DotNet.Sdk.Checkout.Models.Responses;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;

namespace PagSeguro.DotNet.Sdk.Checkout.Providers
{
    /// <inheritdoc cref="ICheckoutProvider" />
    public class CheckoutProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
        : BaseProvider(settings, flurlClient),
        ICheckoutProvider
    {
        /// <inheritdoc />
        public async Task<CheckoutResponse> CreateAsync(CheckoutRequest checkoutRequest)
        {
            return await Request()
                .AppendPathSegment(CheckoutEndpoints.Checkouts)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(checkoutRequest)
                .ReceiveJson<CheckoutResponse>();
        }

        /// <inheritdoc />
        public async Task<CheckoutResponse> GetByIdAsync(string checkoutId)
        {
            return await Request()
                .AppendPathSegment(CheckoutEndpoints.Checkouts)
                .AppendPathSegment(checkoutId)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<CheckoutResponse>();
        }

        /// <inheritdoc />
        public async Task<CheckoutResponse> InactivateAsync(string checkoutId)
        {
            return await Request()
                .AppendPathSegments(CheckoutEndpoints.Checkouts, checkoutId, CheckoutEndpoints.Inactivate)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(new { })
                .ReceiveJson<CheckoutResponse>();
        }

        /// <inheritdoc />
        public async Task<CheckoutResponse> ActivateAsync(string checkoutId)
        {
            return await Request()
                .AppendPathSegments(CheckoutEndpoints.Checkouts, checkoutId, CheckoutEndpoints.Activate)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(new { })
                .ReceiveJson<CheckoutResponse>();
        }
    }
}
