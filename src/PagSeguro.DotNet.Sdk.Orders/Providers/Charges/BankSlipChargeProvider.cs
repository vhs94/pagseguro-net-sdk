using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByBankSlip;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Charges
{
    public class BankSlipChargeProvider(PagSeguroSettings settings)
        : BaseProvider(settings),
        IBankSlipChargeProvider
    {
        public ChargeByBankSlipWriteDto ChargeWriteDto { get; set; } = new ChargeByBankSlipWriteDto();
    }
}
