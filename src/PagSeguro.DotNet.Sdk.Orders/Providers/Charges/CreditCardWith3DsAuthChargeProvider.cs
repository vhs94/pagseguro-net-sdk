using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Charges
{
    /// <inheritdoc cref="ICreditCardWith3DsAuthChargeProvider" />
    public class CreditCardWith3DsAuthChargeProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
        : BaseProvider(settings, flurlClient),
        ICreditCardWith3DsAuthChargeProvider
    {
        /// <inheritdoc />
        public ChargeByCreditCardWith3DsAuthRequest ChargeRequest { get; set; } = new ChargeByCreditCardWith3DsAuthRequest();

        /// <inheritdoc />
        public ICreditCardWith3DsAuthChargeProvider AddPaymentMethod(
            CreditCardWith3DsAuthPaymentMethodRequest creditCardWith3DsAuthPaymentMethodRequest)
        {
            ChargeRequest.PaymentMethod = creditCardWith3DsAuthPaymentMethodRequest;
            return this;
        }

        /// <inheritdoc />
        public ICreditCardWith3DsAuthChargeProvider WithAmount(ChargeAmountRequest chargeAmountRequest)
        {
            ChargeRequest.Amount = chargeAmountRequest;
            return this;
        }

        /// <inheritdoc />
        public ICreditCardWith3DsAuthChargeProvider WithDescription(string description)
        {
            ChargeRequest.Description = description;
            return this;
        }

        /// <inheritdoc />
        public ICreditCardWith3DsAuthChargeProvider WithId(string chargeId)
        {
            ChargeRequest.Id = chargeId;
            return this;
        }

        /// <inheritdoc />
        public ICreditCardWith3DsAuthChargeProvider WithMetadata(IDictionary<string, string> metadata)
        {
            ChargeRequest.Metadata = metadata;
            return this;
        }

        /// <inheritdoc />
        public ICreditCardWith3DsAuthChargeProvider WithNotificationUrl(string notificationUrl)
        {
            ChargeRequest.NotificationUrls.Add(notificationUrl);
            return this;
        }

        /// <inheritdoc />
        public ICreditCardWith3DsAuthChargeProvider WithNotificationUrls(ICollection<string> notificationUrls)
        {
            var newNotificationUrls = ChargeRequest.NotificationUrls.ToList();
            newNotificationUrls.AddRange(notificationUrls);
            ChargeRequest.NotificationUrls = newNotificationUrls;
            return this;
        }

        /// <inheritdoc />
        public ICreditCardWith3DsAuthChargeProvider WithReferenceId(string referenceId)
        {
            ChargeRequest.ReferenceId = referenceId;
            return this;
        }

        /// <inheritdoc />
        public ICreditCardWith3DsAuthChargeProvider Load(ChargeByCreditCardWith3DsAuthRequest chargeRequest)
        {
            ChargeRequest = chargeRequest;
            return this;
        }

        /// <inheritdoc />
        public ChargeByCreditCardWith3DsAuthRequest Build()
        {
            ChargeByCreditCardWith3DsAuthRequest charge = ChargeRequest;
            ChargeRequest = new ChargeByCreditCardWith3DsAuthRequest();
            return charge;
        }

        /// <inheritdoc />
        public async Task<ChargeByCreditCardWith3DsAuthResponse> ChargeAsync()
        {
            ChargeByCreditCardWith3DsAuthResponse chargeResponse = await Request()
                .AppendPathSegments(OrderEndpoint.Charges)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey, Guid.NewGuid())
                .PostJsonAsync(ChargeRequest)
                .ReceiveJson<ChargeByCreditCardWith3DsAuthResponse>();
            ChargeRequest = new ChargeByCreditCardWith3DsAuthRequest();
            return chargeResponse;
        }

        /// <inheritdoc />
        public async Task<ChargeByCreditCardWith3DsAuthResponse> GetByIdAsync(string chargeId)
        {
            return await Request()
                .AppendPathSegments(OrderEndpoint.Charges, chargeId)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<ChargeByCreditCardWith3DsAuthResponse>();
        }

        /// <inheritdoc />
        public async Task<ChargeByCreditCardWith3DsAuthResponse> CancelAsync(int amountValue)
        {
            return await Request()
                .AppendPathSegments(OrderEndpoint.Charges, ChargeRequest.Id, OrderEndpoint.Cancel)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(new
                {
                    amount = new
                    {
                        value = amountValue
                    }
                })
                .ReceiveJson<ChargeByCreditCardWith3DsAuthResponse>();
        }

        /// <inheritdoc />
        public async Task<ChargeByCreditCardWith3DsAuthResponse> CaptureAsync(int amountValue)
        {
            return await Request()
                .AppendPathSegments(OrderEndpoint.Charges, ChargeRequest.Id, OrderEndpoint.Capture)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(new
                {
                    amount = new
                    {
                        value = amountValue
                    }
                })
                .ReceiveJson<ChargeByCreditCardWith3DsAuthResponse>();
        }
    }
}
