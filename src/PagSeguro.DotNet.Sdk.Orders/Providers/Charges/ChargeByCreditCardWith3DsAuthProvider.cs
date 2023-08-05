using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard.Auth3Ds;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Charges
{
    public class ChargeByCreditCardWith3DsAuthProvider
        : GenericChargeProvider<
            ChargeByCreditCardWith3DsAuthWriteDto,
            ChargeByCreditCardWith3DsAuthReadDto>,
        IChargeByCreditCardWith3DsAuthProvider
    {
        public ChargeByCreditCardWith3DsAuthProvider(PagSeguroSettings settings)
            : base(settings)
        {
        }
        public IChargeByCreditCardWith3DsAuthProvider AddPaymentMethod(
            CreditCardWith3DsAuthPaymentMethodWriteDto creditCardWith3DsAuthPaymentMethodWriteDto)
        {
            ChargeWriteDto.PaymentMethod = creditCardWith3DsAuthPaymentMethodWriteDto;
            return this;
        }

        public IChargeByCreditCardWith3DsAuthProvider WithAmount(ChargeAmountWriteDto chargeAmountWriteDto)
        {
            ChargeWriteDto.Amount = chargeAmountWriteDto;
            return this;
        }

        public IChargeByCreditCardWith3DsAuthProvider WithMetadata(IDictionary<string, string> metadata)
        {
            ChargeWriteDto.Metadata = metadata;
            return this;
        }
    }
}
