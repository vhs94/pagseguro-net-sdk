using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces;

namespace PagSeguro.DotNet.Sdk.Orders.Providers
{
    public class OrderProvider : BaseProvider, IOrderProvider
    {
        public OrderProvider(PagSeguroSettings settings)
            : base(settings)
        {
        }

        public async Task<OrderReadDto> CreateOrderAsync(OrderWriteDto orderWriteDto)
        {
            return await BaseUrl
                .AppendPathSegment(OrderEndpoint.Orders)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey, Guid.NewGuid())
                .PostJsonAsync(orderWriteDto)
                .ReceiveJson<OrderReadDto>();
        }
    }
}
