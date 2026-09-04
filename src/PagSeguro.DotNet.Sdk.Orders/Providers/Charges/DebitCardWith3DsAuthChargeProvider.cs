using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Charges
{
    /// <inheritdoc cref="IDebitCardWith3DsAuthChargeProvider" />
    public class DebitCardWith3DsAuthChargeProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
        : BaseProvider(settings, flurlClient),
        IDebitCardWith3DsAuthChargeProvider
    {
        /// <inheritdoc />
        public ChargeByDebitCardWith3DsAuthRequest ChargeRequest { get; set; } = new ChargeByDebitCardWith3DsAuthRequest();

        /// <inheritdoc />
        public IDebitCardWith3DsAuthChargeProvider AddPaymentMethod(
            DebitCardWith3DsAuthPaymentMethodRequest debitCardWith3DsAuthPaymentMethodRequest)
        {
            ChargeRequest.PaymentMethod = debitCardWith3DsAuthPaymentMethodRequest;
            return this;
        }

        /// <inheritdoc />
        public IDebitCardWith3DsAuthChargeProvider WithAmount(ChargeAmountRequest chargeAmountRequest)
        {
            ChargeRequest.Amount = chargeAmountRequest;
            return this;
        }

        /// <inheritdoc />
        public IDebitCardWith3DsAuthChargeProvider WithDescription(string description)
        {
            ChargeRequest.Description = description;
            return this;
        }

        /// <inheritdoc />
        public IDebitCardWith3DsAuthChargeProvider WithId(string chargeId)
        {
            ChargeRequest.Id = chargeId;
            return this;
        }

        /// <inheritdoc />
        public IDebitCardWith3DsAuthChargeProvider WithMetadata(IDictionary<string, string> metadata)
        {
            ChargeRequest.Metadata = metadata;
            return this;
        }

        /// <inheritdoc />
        public IDebitCardWith3DsAuthChargeProvider WithNotificationUrl(string notificationUrl)
        {
            ChargeRequest.NotificationUrls.Add(notificationUrl);
            return this;
        }

        /// <inheritdoc />
        public IDebitCardWith3DsAuthChargeProvider WithNotificationUrls(ICollection<string> notificationUrls)
        {
            var newNotificationUrls = ChargeRequest.NotificationUrls.ToList();
            newNotificationUrls.AddRange(notificationUrls);
            ChargeRequest.NotificationUrls = newNotificationUrls;
            return this;
        }

        /// <inheritdoc />
        public IDebitCardWith3DsAuthChargeProvider WithReferenceId(string referenceId)
        {
            ChargeRequest.ReferenceId = referenceId;
            return this;
        }

        /// <inheritdoc />
        public IDebitCardWith3DsAuthChargeProvider Load(ChargeByDebitCardWith3DsAuthRequest chargeRequest)
        {
            ChargeRequest = chargeRequest;
            return this;
        }

        /// <inheritdoc />
        public ChargeByDebitCardWith3DsAuthRequest Build()
        {
            ChargeByDebitCardWith3DsAuthRequest charge = ChargeRequest;
            ChargeRequest = new ChargeByDebitCardWith3DsAuthRequest();
            return charge;
        }

        /// <inheritdoc />
        public async Task<ChargeByDebitCardWith3DsAuthResponse> ChargeAsync()
        {
            ChargeByDebitCardWith3DsAuthResponse chargeResponse = await Request()
                .AppendPathSegments(OrderEndpoint.Charges)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey, Guid.NewGuid())
                .PostJsonAsync(ChargeRequest)
                .ReceiveJson<ChargeByDebitCardWith3DsAuthResponse>();
            ChargeRequest = new ChargeByDebitCardWith3DsAuthRequest();
            return chargeResponse;
        }

        /// <inheritdoc />
        public async Task<ChargeByDebitCardWith3DsAuthResponse> GetByIdAsync(string chargeId)
        {
            return await Request()
                .AppendPathSegments(OrderEndpoint.Charges, chargeId)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<ChargeByDebitCardWith3DsAuthResponse>();
        }

        /// <inheritdoc />
        public async Task<ChargeByDebitCardWith3DsAuthResponse> CancelAsync(int amountValue)
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
                .ReceiveJson<ChargeByDebitCardWith3DsAuthResponse>();
        }
    }
}
