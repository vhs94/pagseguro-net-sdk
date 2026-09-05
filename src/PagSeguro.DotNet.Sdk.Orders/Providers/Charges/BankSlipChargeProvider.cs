using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Charges
{
    /// <inheritdoc cref="IBankSlipChargeProvider" />
    public class BankSlipChargeProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
        : BaseProvider(settings, flurlClient),
        IBankSlipChargeProvider
    {
        /// <inheritdoc />
        public ChargeByBankSlipRequest ChargeRequest { get; set; } = new ChargeByBankSlipRequest();

        /// <inheritdoc />
        public IBankSlipChargeProvider AddBankSlip(BankSlipRequest bankSlipRequest)
        {
            ChargeRequest.PaymentMethod = new BankSlipPaymentMethodRequest
            {
                BankSlip = bankSlipRequest
            };
            return this;
        }

        /// <inheritdoc />
        public IBankSlipChargeProvider WithAmount(ChargeAmountRequest chargeAmountRequest)
        {
            ChargeRequest.Amount = chargeAmountRequest;
            return this;
        }

        /// <inheritdoc />
        public IBankSlipChargeProvider WithDescription(string description)
        {
            ChargeRequest.Description = description;
            return this;
        }

        /// <inheritdoc />
        public IBankSlipChargeProvider WithId(string chargeId)
        {
            ChargeRequest.Id = chargeId;
            return this;
        }

        /// <inheritdoc />
        public IBankSlipChargeProvider WithNotificationUrl(string notificationUrl)
        {
            ChargeRequest.NotificationUrls.Add(notificationUrl);
            return this;
        }

        /// <inheritdoc />
        public IBankSlipChargeProvider WithNotificationUrls(ICollection<string> notificationUrls)
        {
            var newNotificationUrls = ChargeRequest.NotificationUrls.ToList();
            newNotificationUrls.AddRange(notificationUrls);
            ChargeRequest.NotificationUrls = newNotificationUrls;
            return this;
        }

        /// <inheritdoc />
        public IBankSlipChargeProvider WithReferenceId(string referenceId)
        {
            ChargeRequest.ReferenceId = referenceId;
            return this;
        }

        /// <inheritdoc />
        public IBankSlipChargeProvider Load(ChargeByBankSlipRequest chargeRequest)
        {
            ChargeRequest = chargeRequest;
            return this;
        }

        /// <inheritdoc />
        public ChargeByBankSlipRequest Build()
        {
            ChargeByBankSlipRequest charge = ChargeRequest;
            ChargeRequest = new ChargeByBankSlipRequest();
            return charge;
        }

        /// <inheritdoc />
        public async Task<ChargeByBankSlipResponse> ChargeAsync()
        {
            ChargeByBankSlipResponse chargeResponse = await Request()
                .AppendPathSegments(OrderEndpoint.Charges)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey, Guid.NewGuid())
                .PostJsonAsync(ChargeRequest)
                .ReceiveJson<ChargeByBankSlipResponse>();
            ChargeRequest = new ChargeByBankSlipRequest();
            return chargeResponse;
        }

        /// <inheritdoc />
        public async Task<ChargeByBankSlipResponse> GetByIdAsync(string chargeId)
        {
            return await Request()
                .AppendPathSegments(OrderEndpoint.Charges, chargeId)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<ChargeByBankSlipResponse>();
        }

        /// <inheritdoc />
        public async Task<ChargeByBankSlipResponse> CancelAsync(int amountValue)
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
                .ReceiveJson<ChargeByBankSlipResponse>();
        }
    }
}
