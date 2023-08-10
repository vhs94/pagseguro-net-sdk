using AutoMapper;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByBankSlip;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Orders
{
    public class BankSlipOrderProvider
        : ChargedOrderProviderOf<
            ChargeByBankSlipWriteDto,
            ChargeByBankSlipReadDto>,
        IBankSlipOrderProvider
    {
        public BankSlipOrderProvider(
            PagSeguroSettings settings,
            IMapper mapper)
            : base(settings, mapper)
        {
        }
    }
}
