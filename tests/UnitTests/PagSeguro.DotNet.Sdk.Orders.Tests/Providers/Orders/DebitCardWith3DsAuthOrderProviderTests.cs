using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.DebitCard;
using PagSeguro.DotNet.Sdk.Orders.Providers.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Orders
{
    public class DebitCardWith3DsAuthOrderProviderTests
        : GenericOrderProviderTests<
            ChargeByDebitCardWith3DsAuthWriteDto,
            ChargeByDebitCardWith3DsAuthReadDto>
    {
        protected override GenericOrderProvider<ChargeByDebitCardWith3DsAuthWriteDto, ChargeByDebitCardWith3DsAuthReadDto> CreateProvider()
        {
            return new DebitCardWith3DsAuthOrderProvider(Settings, MapperMock);
        }
    }
}
