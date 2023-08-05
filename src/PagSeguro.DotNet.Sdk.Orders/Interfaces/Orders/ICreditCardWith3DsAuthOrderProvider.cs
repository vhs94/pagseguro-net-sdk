using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders
{
    public interface ICreditCardWith3DsAuthOrderProvider
        : IGenericOrderProvider<
            ChargeByCreditCardWith3DsAuthWriteDto,
            ChargeByCreditCardWith3DsAuthReadDto>
    {
    }
}
