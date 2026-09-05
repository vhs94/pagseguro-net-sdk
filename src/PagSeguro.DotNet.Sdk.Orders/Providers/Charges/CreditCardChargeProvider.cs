using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Charges
{
    /// <inheritdoc cref="ICreditCardChargeProvider" />
    public class CreditCardChargeProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
        : BaseProvider(settings, flurlClient),
        ICreditCardChargeProvider
    {
        /// <inheritdoc />
        public ChargeByCreditCardRequest ChargeRequest { get; set; } = new ChargeByCreditCardRequest();

        /// <inheritdoc />
        public ICreditCardChargeProvider AddPaymentMethod(
            CreditCardPaymentMethodRequest creditCardPaymentMethodRequest)
        {
            ChargeRequest.PaymentMethod = creditCardPaymentMethodRequest;
            return this;
        }

        /// <inheritdoc />
        public ICreditCardChargeProvider WithAmount(ChargeAmountRequest chargeAmountRequest)
        {
            ChargeRequest.Amount = chargeAmountRequest;
            return this;
        }

        /// <inheritdoc />
        public ICreditCardChargeProvider WithDescription(string description)
        {
            ChargeRequest.Description = description;
            return this;
        }

        /// <inheritdoc />
        public ICreditCardChargeProvider WithId(string chargeId)
        {
            ChargeRequest.Id = chargeId;
            return this;
        }

        /// <inheritdoc />
        public ICreditCardChargeProvider WithMetadata(IDictionary<string, string> metadata)
        {
            ChargeRequest.Metadata = metadata;
            return this;
        }

        /// <inheritdoc />
        public ICreditCardChargeProvider WithNotificationUrl(string notificationUrl)
        {
            ChargeRequest.NotificationUrls.Add(notificationUrl);
            return this;
        }

        /// <inheritdoc />
        public ICreditCardChargeProvider WithNotificationUrls(ICollection<string> notificationUrls)
        {
            var newNotificationUrls = ChargeRequest.NotificationUrls.ToList();
            newNotificationUrls.AddRange(notificationUrls);
            ChargeRequest.NotificationUrls = newNotificationUrls;
            return this;
        }

        /// <inheritdoc />
        public ICreditCardChargeProvider WithReferenceId(string referenceId)
        {
            ChargeRequest.ReferenceId = referenceId;
            return this;
        }

        /// <inheritdoc />
        public ICreditCardChargeProvider Load(ChargeByCreditCardRequest chargeRequest)
        {
            ChargeRequest = chargeRequest;
            return this;
        }

        /// <inheritdoc />
        public ChargeByCreditCardRequest Build()
        {
            ChargeByCreditCardRequest charge = ChargeRequest;
            ChargeRequest = new ChargeByCreditCardRequest();
            return charge;
        }

        /// <inheritdoc />
        public async Task<ChargeByCreditCardResponse> ChargeAsync()
        {
            ChargeByCreditCardResponse chargeResponse = await Request()
                .AppendPathSegments(OrderEndpoint.Charges)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey, Guid.NewGuid())
                .PostJsonAsync(ChargeRequest)
                .ReceiveJson<ChargeByCreditCardResponse>();
            ChargeRequest = new ChargeByCreditCardRequest();
            return chargeResponse;
        }

        /// <inheritdoc />
        public async Task<ChargeByCreditCardResponse> GetByIdAsync(string chargeId)
        {
            return await Request()
                .AppendPathSegments(OrderEndpoint.Charges, chargeId)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<ChargeByCreditCardResponse>();
        }

        /// <inheritdoc />
        public async Task<ChargeByCreditCardResponse> CancelAsync(int amountValue)
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
                .ReceiveJson<ChargeByCreditCardResponse>();
        }

        /// <inheritdoc />
        public async Task<ChargeByCreditCardResponse> CaptureAsync(int amountValue)
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
                .ReceiveJson<ChargeByCreditCardResponse>();
        }
    }
}
