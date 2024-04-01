using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard.Auth3Ds;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods
{
    public interface ICreditCardWith3DsAuthChargeProvider
        : ICreditCardChargeProviderOf<
            ICreditCardWith3DsAuthChargeProvider,
            ChargeByCreditCardWith3DsAuthWriteDto,
            ChargeByCreditCardWith3DsAuthReadDto>
    {
        public ICreditCardWith3DsAuthChargeProvider AddPaymentMethod(
            CreditCardWith3DsAuthPaymentMethodWriteDto creditCardWith3DsAuthPaymentMethodWriteDto)
        {
            ChargeWriteDto.PaymentMethod = creditCardWith3DsAuthPaymentMethodWriteDto;
            return this;
        }
    }
}
