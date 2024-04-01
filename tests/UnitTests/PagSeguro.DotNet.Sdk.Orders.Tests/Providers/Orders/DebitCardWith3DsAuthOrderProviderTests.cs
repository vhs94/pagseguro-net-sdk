using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.DebitCard;
using PagSeguro.DotNet.Sdk.Orders.Providers.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Orders
{
    public class DebitCardWith3DsAuthOrderProviderTests
        : ChargedOrderProviderOfTests<
            ChargeByDebitCardWith3DsAuthWriteDto,
            ChargeByDebitCardWith3DsAuthReadDto>
    {
        protected override DebitCardWith3DsAuthOrderProvider CreateProvider()
        {
            return new DebitCardWith3DsAuthOrderProvider(Settings, MapperMock);
        }
    }
}
