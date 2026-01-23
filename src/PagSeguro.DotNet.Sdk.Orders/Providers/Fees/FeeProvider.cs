using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Fees;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Fees
{
    public class FeeProvider(PagSeguroSettings settings) : BaseProvider(settings), IFeeProvider
    {
        public required FeeRequest Fee { get; set; }

        public IFeeProvider WithCreditCardBin(int creditCardBin)
        {
            Fee.CreditCardBin = creditCardBin;
            return this;
        }

        public IFeeProvider WithMaxInstallments(int maxInstallments)
        {
            Fee.MaxInstallments = maxInstallments;
            return this;
        }

        public IFeeProvider WithMaxInstallmentsNoInterest(int maxInstallmentsNoInterest)
        {
            Fee.MaxInstallmentsNoInterest = maxInstallmentsNoInterest;
            return this;
        }

        public IFeeProvider WithValue(int amountValue)
        {
            Fee.Value = amountValue;
            return this;
        }

        public async Task<FeeResponse> CalculateAsync()
        {
            var response = await BaseUrl
                .AppendPathSegments(OrderEndpoint.Charges, OrderEndpoint.CalculateFee)
                .WithOAuthBearerToken(Settings.Token)
                .SetQueryParam("payment_methods", Fee.PaymentMethods)
                .SetQueryParam("value", Fee.Value)
                .SetQueryParam("max_installments", Fee.MaxInstallments)
                .SetQueryParam("max_installments_no_interest", Fee.MaxInstallmentsNoInterest)
                .SetQueryParam("credit_card_bin", Fee.CreditCardBin)
                .GetJsonAsync<FeeResponse>();
            Fee = new FeeRequest();
            return response;
        }
    }
}
