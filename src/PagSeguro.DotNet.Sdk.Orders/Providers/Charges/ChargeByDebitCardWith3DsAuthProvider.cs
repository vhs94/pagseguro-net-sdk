using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.DebitCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.DebitCard;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Charges
{
    public class ChargeByDebitCardWith3DsAuthProvider
        : GenericChargeProvider<
            ChargeByDebitCardWith3DsAuthWriteDto,
            ChargeByDebitCardWith3DsAuthReadDto>,
        IChargeByDebitCardWith3DsAuthProvider
    {
        public ChargeByDebitCardWith3DsAuthProvider(PagSeguroSettings settings)
            : base(settings)
        {
        }

        public IChargeByDebitCardWith3DsAuthProvider AddPaymentMethod(
            DebitCardWith3DsAuthPaymentMethodWriteDto debitCardWith3DsAuthPaymentMethodWriteDto)
        {
            ChargeWriteDto.PaymentMethod = debitCardWith3DsAuthPaymentMethodWriteDto;
            return this;
        }

        public IChargeByDebitCardWith3DsAuthProvider WithAmount(ChargeAmountWriteDto chargeAmountWriteDto)
        {
            ChargeWriteDto.Amount = chargeAmountWriteDto;
            return this;
        }

        public IChargeByDebitCardWith3DsAuthProvider WithMetadata(IDictionary<string, string> metadata)
        {
            ChargeWriteDto.Metadata = metadata;
            return this;
        }
    }
}
