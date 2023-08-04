using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByBankSlip;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.DebitCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Orders
{
    public class OrderProvider : BaseProvider, IOrderProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public OrderProvider(
            PagSeguroSettings settings,
            IServiceProvider serviceProvider)
            : base(settings)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<OrderReadDto> CreateAsync(OrderWriteDto orderWriteDto)
        {
            return await BaseUrl
                .AppendPathSegment(OrderEndpoint.Orders)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey, Guid.NewGuid())
                .PostJsonAsync(orderWriteDto)
                .ReceiveJson<OrderReadDto>();
        }

        public async Task<OrderReadDto> GetByIdAsync(string orderId)
        {
            return await BaseUrl
                .AppendPathSegment(OrderEndpoint.Orders)
                .AppendPathSegment(orderId)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<OrderReadDto>();
        }

        public IGenericOrderProvider<ChargeByBankSlipWriteDto, ChargeByBankSlipReadDto> WithBankSlip()
        {
            return _serviceProvider.GetService<IGenericOrderProvider<ChargeByBankSlipWriteDto, ChargeByBankSlipReadDto>>();
        }

        public IGenericOrderProvider<ChargeByCreditCardWriteDto, ChargeByCreditCardReadDto> WithCreditCard()
        {
            return _serviceProvider.GetService<IGenericOrderProvider<ChargeByCreditCardWriteDto, ChargeByCreditCardReadDto>>();
        }

        public IGenericOrderProvider<ChargeByCreditCardWith3DsAuthWriteDto, ChargeByCreditCardWith3DsAuthReadDto> WithCreditCardAnd3DsAuthentication()
        {
            return _serviceProvider.GetService<IGenericOrderProvider<ChargeByCreditCardWith3DsAuthWriteDto, ChargeByCreditCardWith3DsAuthReadDto>>();
        }

        public IGenericOrderProvider<ChargeByDebitCardWith3DsAuthWriteDto, ChargeByDebitCardWith3DsAuthReadDto> WithDebitCardAnd3DsAuthentication()
        {
            return _serviceProvider.GetService<IGenericOrderProvider<ChargeByDebitCardWith3DsAuthWriteDto, ChargeByDebitCardWith3DsAuthReadDto>>();
        }
    }
}
