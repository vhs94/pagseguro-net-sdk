using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges
{
    public interface IChargeByCreditCardProvider
        : IGenericChargeProvider<
            ChargeByCreditCardWriteDto,
            ChargeByCreditCardReadDto>
    {
        IChargeByCreditCardProvider AddPaymentMethod(CreditCardPaymentMethodWriteDto creditCardPaymentMethodWriteDto);
        IChargeByCreditCardProvider WithAmount(ChargeAmountWriteDto chargeAmountWriteDto);
        IChargeByCreditCardProvider WithMetadata(IDictionary<string, string> metadata);
    }
}
