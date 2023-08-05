using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Providers.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Orders
{
    public class CreditCardWith3DsAuthOrderProviderTests
        : GenericOrderProviderTests<
            ChargeByCreditCardWith3DsAuthWriteDto,
            ChargeByCreditCardWith3DsAuthReadDto>
    {
        protected override GenericOrderProvider<ChargeByCreditCardWith3DsAuthWriteDto, ChargeByCreditCardWith3DsAuthReadDto> CreateProvider()
        {
            return new CreditCardWith3DsAuthOrderProvider(Settings);
        }
    }
}
