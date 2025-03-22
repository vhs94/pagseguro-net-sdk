using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Charges
{
    public class CreditCardWith3DsAuthChargeProvider(PagSeguroSettings settings)
        : BaseProvider(settings),
        ICreditCardWith3DsAuthChargeProvider
    {
        public ChargeByCreditCardWith3DsAuthWriteDto ChargeWriteDto { get; set; } = new ChargeByCreditCardWith3DsAuthWriteDto();
    }
}
