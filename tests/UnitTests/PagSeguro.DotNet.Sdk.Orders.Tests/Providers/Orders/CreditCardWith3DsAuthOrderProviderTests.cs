using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Providers.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Orders
{
    public class CreditCardWith3DsAuthOrderProviderTests
        : ChargedOrderProviderOfTests<
            ChargeByCreditCardWith3DsAuthWriteDto,
            ChargeByCreditCardWith3DsAuthReadDto>
    {
        protected override CreditCardWith3DsAuthOrderProvider CreateProvider()
        {
            return new CreditCardWith3DsAuthOrderProvider(Settings, MapperMock);
        }
    }
}
