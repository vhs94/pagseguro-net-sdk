using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Charges
{
    public class ChargeByCreditCardProvider
        : GenericChargeProvider<
            ChargeByCreditCardWriteDto,
            ChargeByCreditCardReadDto>,
        IChargeByCreditCardProvider
    {
        public ChargeByCreditCardProvider(PagSeguroSettings settings)
            : base(settings)
        {
        }

        public IChargeByCreditCardProvider AddPaymentMethod(
            CreditCardPaymentMethodWriteDto creditCardPaymentMethodWriteDto)
        {
            ChargeWriteDto.PaymentMethod = creditCardPaymentMethodWriteDto;
            return this;
        }

        public IChargeByCreditCardProvider WithAmount(ChargeAmountWriteDto chargeAmountWriteDto)
        {
            ChargeWriteDto.Amount = chargeAmountWriteDto;
            return this;
        }

        public IChargeByCreditCardProvider WithMetadata(IDictionary<string, string> metadata)
        {
            ChargeWriteDto.Metadata = metadata;
            return this;
        }
    }
}
