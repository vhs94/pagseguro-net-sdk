using AutoMapper;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Orders
{
    public class CreditCardWith3DsAuthOrderProvider
        : ChargedOrderProviderOf<
            ChargeByCreditCardWith3DsAuthWriteDto,
            ChargeByCreditCardWith3DsAuthReadDto>,
        ICreditCardWith3DsAuthOrderProvider
    {
        public CreditCardWith3DsAuthOrderProvider(
            PagSeguroSettings settings,
            IMapper mapper)
            : base(settings, mapper)
        {
        }
    }
}
