using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Providers.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Orders
{
    public class CreditCardOrderProviderTests : ChargedOrderProviderOfTests<ChargeByCreditCardWriteDto, ChargeByCreditCardReadDto>
    {
        protected override CreditCardOrderProvider CreateProvider()
        {
            return new CreditCardOrderProvider(Settings, MapperMock);
        }
    }
}
