using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.DebitCard;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Charges
{
    public class DebitCardWith3DsAuthChargeProvider(PagSeguroSettings settings)
        : BaseProvider(settings),
        IDebitCardWith3DsAuthChargeProvider
    {
        public ChargeByDebitCardWith3DsAuthWriteDto ChargeWriteDto { get; set; } = new ChargeByDebitCardWith3DsAuthWriteDto();
    }
}
