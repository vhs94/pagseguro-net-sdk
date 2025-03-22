using AutoMapper;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Orders
{
    public class CreditCardWith3DsAuthOrderProvider(
        PagSeguroSettings settings,
        IMapper mapper)
        : ChargedOrderProviderOf<
            ChargeByCreditCardWith3DsAuthWriteDto,
            ChargeByCreditCardWith3DsAuthReadDto>(settings, mapper),
        ICreditCardWith3DsAuthOrderProvider
    {
    }
}
