using Flurl;
using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Fees;
using PagSeguro.DotNet.Sdk.Orders.Helpers;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Fees
{
    public interface IFeeProvider : IProvider, IBuilder<IFeeProvider, FeeWriteDto>
    {
        public IFeeProvider WithCreditCardBin(int creditCardBin)
        {
            Entity.CreditCardBin = creditCardBin;
            return this;
        }

        public IFeeProvider WithMaxInstallments(int maxInstallments)
        {
            Entity.MaxInstallments = maxInstallments;
            return this;
        }

        public IFeeProvider WithMaxInstallmentsNoInterest(int maxInstallmentsNoInterest)
        {
            Entity.MaxInstallmentsNoInterest = maxInstallmentsNoInterest;
            return this;
        }

        public IFeeProvider WithValue(int amountValue)
        {
            Entity.Value = amountValue;
            return this;
        }

        public async Task<FeeReadDto> CalculateAsync()
        {
            FeeWriteDto feeWriteDto = Build();
            return await BaseUrl
                .AppendPathSegments(OrderEndpoint.Charges, OrderEndpoint.CalculateFee)
                .WithOAuthBearerToken(Settings.Token)
                .SetQueryParam("payment_methods", feeWriteDto.PaymentMethods)
                .SetQueryParam("value", feeWriteDto.Value)
                .SetQueryParam("max_installments", feeWriteDto.MaxInstallments)
                .SetQueryParam("max_installments_no_interest", feeWriteDto.MaxInstallmentsNoInterest)
                .SetQueryParam("credit_card_bin", feeWriteDto.CreditCardBin)
                .GetJsonAsync<FeeReadDto>();
        }
    }
}
