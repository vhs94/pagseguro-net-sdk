using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders
{
    public interface ICreditCardWith3DsAuthOrderProvider
        : IChargedOrderProviderOf<
            ChargeByCreditCardWith3DsAuthWriteDto,
            ChargeByCreditCardWith3DsAuthReadDto>
    {
    }
}
