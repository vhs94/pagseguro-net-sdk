using Flurl;
using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Fees;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Fees
{
    /// <inheritdoc cref="IFeeProvider" />
    public class FeeProvider : BaseProvider, IFeeProvider
    {
        /// <inheritdoc />
        public FeeRequest Entity { get; set; } = null!;

        public FeeProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
            : base(settings, flurlClient) => Reset();

        /// <inheritdoc />
        public void Reset() => Entity = new FeeRequest();

        /// <inheritdoc />
        public IFeeProvider WithCreditCardBin(int creditCardBin)
        {
            Entity.CreditCardBin = creditCardBin;
            return this;
        }

        /// <inheritdoc />
        public IFeeProvider WithMaxInstallments(int maxInstallments)
        {
            Entity.MaxInstallments = maxInstallments;
            return this;
        }

        /// <inheritdoc />
        public IFeeProvider WithMaxInstallmentsNoInterest(int maxInstallmentsNoInterest)
        {
            Entity.MaxInstallmentsNoInterest = maxInstallmentsNoInterest;
            return this;
        }

        /// <inheritdoc />
        public IFeeProvider WithValue(int amountValue)
        {
            Entity.Value = amountValue;
            return this;
        }

        /// <inheritdoc />
        public IFeeProvider Load(FeeRequest entity)
        {
            Entity = entity;
            return this;
        }

        /// <inheritdoc />
        public FeeRequest Build()
        {
            FeeRequest entity = Entity;
            Reset();
            return entity;
        }

        /// <inheritdoc />
        public async Task<FeeResponse> CalculateAsync()
        {
            FeeRequest feeRequest = Build();
            return await Request()
                .AppendPathSegments(OrderEndpoint.Charges, OrderEndpoint.CalculateFee)
                .WithOAuthBearerToken(Settings.Token)
                .SetQueryParam("payment_methods", feeRequest.PaymentMethods)
                .SetQueryParam("value", feeRequest.Value)
                .SetQueryParam("max_installments", feeRequest.MaxInstallments)
                .SetQueryParam("max_installments_no_interest", feeRequest.MaxInstallmentsNoInterest)
                .SetQueryParam("credit_card_bin", feeRequest.CreditCardBin)
                .GetJsonAsync<FeeResponse>();
        }
    }
}
