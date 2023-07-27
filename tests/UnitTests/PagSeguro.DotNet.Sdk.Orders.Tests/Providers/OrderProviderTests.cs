using AutoFixture;
using FluentAssertions;
using Flurl;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Providers;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers
{
    public class OrderProviderTests : BaseProviderTests<OrderProvider>
    {
        private OrderReadDto _orderReadDto;

        protected override OrderProvider CreateProvider()
        {
            return new OrderProvider(Settings);
        }

        protected override void SetupMocks()
        {
            _orderReadDto = CreateOrderReadDto();
            HttpTestMock
                .ForCallsTo(Url.Combine(Provider.BaseUrl, OrderEndpoint.Orders))
                .RespondWithJson(_orderReadDto);
        }

        private OrderReadDto CreateOrderReadDto()
        {
            return Fixture.Create<OrderReadDto>();
        }

        [Fact]
        public async Task CreateApplicationAsync_OrderIsValid_HttpRequestIsCreated()
        {
            OrderWriteDto orderWriteDto = CreateOrderWriteDto();

            OrderReadDto result = await Provider.CreateOrderAsync(orderWriteDto);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, OrderEndpoint.Orders))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(orderWriteDto)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(_orderReadDto);
        }

        private OrderWriteDto CreateOrderWriteDto()
        {
            return Fixture.Create<OrderWriteDto>();
        }
    }
}
