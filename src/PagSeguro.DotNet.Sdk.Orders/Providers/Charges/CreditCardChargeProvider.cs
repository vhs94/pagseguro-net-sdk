using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Charges
{
    public class CreditCardChargeProvider : BaseProvider, ICreditCardChargeProvider
    {
        public ChargeByCreditCardWriteDto ChargeWriteDto { get; set; }

        public CreditCardChargeProvider(PagSeguroSettings settings)
            : base(settings)
        {
            ChargeWriteDto = new ChargeByCreditCardWriteDto();
        }
    }
}
