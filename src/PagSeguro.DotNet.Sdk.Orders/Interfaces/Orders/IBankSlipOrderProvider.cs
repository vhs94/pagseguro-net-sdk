using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByBankSlip;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders
{
    public interface IBankSlipOrderProvider : IChargedOrderProviderOf<ChargeByBankSlipWriteDto, ChargeByBankSlipReadDto>
    {
    }
}
