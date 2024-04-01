using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.DebitCard;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders
{
    public interface IDebitCardWith3DsAuthOrderProvider
        : IChargedOrderProviderOf<
            ChargeByDebitCardWith3DsAuthWriteDto,
            ChargeByDebitCardWith3DsAuthReadDto>
    {
    }
}
