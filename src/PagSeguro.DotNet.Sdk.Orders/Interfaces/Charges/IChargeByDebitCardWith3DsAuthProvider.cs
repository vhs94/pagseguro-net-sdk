using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.DebitCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.DebitCard;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges
{
    public interface IChargeByDebitCardWith3DsAuthProvider
        : IGenericChargeProvider<
            ChargeByDebitCardWith3DsAuthWriteDto,
            ChargeByDebitCardWith3DsAuthReadDto>
    {
        IChargeByDebitCardWith3DsAuthProvider AddPaymentMethod(
            DebitCardWith3DsAuthPaymentMethodWriteDto debitCardWith3DsAuthPaymentMethodWriteDto);
        IChargeByDebitCardWith3DsAuthProvider WithAmount(ChargeAmountWriteDto chargeAmountWriteDto);
        IChargeByDebitCardWith3DsAuthProvider WithMetadata(IDictionary<string, string> metadata);
    }
}
