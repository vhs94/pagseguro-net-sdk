using AutoMapper;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Orders
{
    public class CreditCardOrderProvider(
        PagSeguroSettings settings,
        IMapper mapper)
        : ChargedOrderProviderOf<
            ChargeByCreditCardWriteDto,
            ChargeByCreditCardReadDto>(settings, mapper),
        ICreditCardOrderProvider
    {
    }
}
