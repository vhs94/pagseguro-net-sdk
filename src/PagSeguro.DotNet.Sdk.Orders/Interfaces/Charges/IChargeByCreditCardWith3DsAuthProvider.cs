using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard.Auth3Ds;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges
{
    public interface IChargeByCreditCardWith3DsAuthProvider
        : IGenericChargeProvider<
            ChargeByCreditCardWith3DsAuthWriteDto,
            ChargeByCreditCardWith3DsAuthReadDto>
    {
        IChargeByCreditCardWith3DsAuthProvider AddPaymentMethod(
            CreditCardWith3DsAuthPaymentMethodWriteDto creditCardWith3DsAuthPaymentMethodWriteDto);
        IChargeByCreditCardWith3DsAuthProvider WithAmount(ChargeAmountWriteDto chargeAmountWriteDto);
        IChargeByCreditCardWith3DsAuthProvider WithMetadata(IDictionary<string, string> metadata);
    }
}
