using AutoMapper;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByBankSlip;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Orders
{
    public class BankSlipOrderProvider(
        PagSeguroSettings settings,
        IMapper mapper)
        : ChargedOrderProviderOf<
            ChargeByBankSlipWriteDto,
            ChargeByBankSlipReadDto>(settings, mapper),
        IBankSlipOrderProvider
    {
    }
}
