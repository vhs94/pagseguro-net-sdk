using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Orders
{
    public class CreditCardOrderProvider
        : GenericOrderProvider<
            ChargeByCreditCardWriteDto,
            ChargeByCreditCardReadDto>,
        ICreditCardOrderProvider
    {
        public CreditCardOrderProvider(PagSeguroSettings settings)
            : base(settings)
        {
        }
    }
}
