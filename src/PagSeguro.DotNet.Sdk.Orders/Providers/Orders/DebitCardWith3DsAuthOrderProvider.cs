using AutoMapper;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.DebitCard;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Orders
{
    public class DebitCardWith3DsAuthOrderProvider(
        PagSeguroSettings settings,
        IMapper mapper)
        : ChargedOrderProviderOf<
            ChargeByDebitCardWith3DsAuthWriteDto,
            ChargeByDebitCardWith3DsAuthReadDto>(settings, mapper),
        IDebitCardWith3DsAuthOrderProvider
    {
    }
}
