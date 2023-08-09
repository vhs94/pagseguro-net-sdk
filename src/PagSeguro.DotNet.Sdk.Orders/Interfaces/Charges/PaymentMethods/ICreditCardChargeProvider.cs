using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods
{
    public interface ICreditCardChargeProvider
        : ICreditCardChargeProviderOf<
            ICreditCardChargeProvider,
            ChargeByCreditCardWriteDto,
            ChargeByCreditCardReadDto>
    {
        public ICreditCardChargeProvider AddPaymentMethod(
            CreditCardPaymentMethodWriteDto creditCardPaymentMethodWriteDto)
        {
            ChargeWriteDto.PaymentMethod = creditCardPaymentMethodWriteDto;
            return this;
        }
    }
}
