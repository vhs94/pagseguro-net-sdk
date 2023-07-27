using AutoFixture;
using Flurl;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Providers;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers
{
    public class OrderProviderTests : BaseProviderTests<OrderProvider>
    {
        protected override OrderProvider CreateProvider()
        {
            return new OrderProvider(Settings);
        }

        [Fact]
        public async Task CreateApplicationAsync_OrderIsValid_HttpRequestIsCreated()
        {
            var orderWriteDto = CreateOrderWriteDto();

            await Provider.CreateOrderAsync(orderWriteDto);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, OrderEndpoint.Orders))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(orderWriteDto)
                .Times(1);
        }

        private OrderWriteDto CreateOrderWriteDto()
        {
            return Fixture.Create<OrderWriteDto>();
        }
    }
}
