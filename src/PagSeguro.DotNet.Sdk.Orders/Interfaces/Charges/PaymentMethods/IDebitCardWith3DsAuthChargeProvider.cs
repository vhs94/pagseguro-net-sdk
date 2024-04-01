using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.DebitCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.DebitCard;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods
{
    public interface IDebitCardWith3DsAuthChargeProvider
        : ICardChargeProviderOf<
            IDebitCardWith3DsAuthChargeProvider,
            ChargeByDebitCardWith3DsAuthWriteDto,
            ChargeByDebitCardWith3DsAuthReadDto>
    {
        public IDebitCardWith3DsAuthChargeProvider AddPaymentMethod(
            DebitCardWith3DsAuthPaymentMethodWriteDto debitCardWith3DsAuthPaymentMethodWriteDto)
        {
            ChargeWriteDto.PaymentMethod = debitCardWith3DsAuthPaymentMethodWriteDto;
            return this;
        }
    }
}
