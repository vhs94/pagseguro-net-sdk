using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.DebitCard;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Charges
{
    public class DebitCardWith3DsAuthChargeProvider : BaseProvider, IDebitCardWith3DsAuthChargeProvider
    {
        public ChargeByDebitCardWith3DsAuthWriteDto ChargeWriteDto { get; set; }

        public DebitCardWith3DsAuthChargeProvider(PagSeguroSettings settings)
            : base(settings)
        {
            ChargeWriteDto = new ChargeByDebitCardWith3DsAuthWriteDto();
        }
    }
}
