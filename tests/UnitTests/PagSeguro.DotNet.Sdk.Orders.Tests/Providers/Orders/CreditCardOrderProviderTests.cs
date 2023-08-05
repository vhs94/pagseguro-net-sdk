using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Providers.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Orders
{
    public class CreditCardOrderProviderTests : GenericOrderProviderTests<ChargeByCreditCardWriteDto, ChargeByCreditCardReadDto>
    {
        protected override GenericOrderProvider<ChargeByCreditCardWriteDto, ChargeByCreditCardReadDto> CreateProvider()
        {
            return new CreditCardOrderProvider(Settings, MapperMock);
        }
    }
}
