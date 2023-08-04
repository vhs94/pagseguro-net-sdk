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
                .ForCallsTo(
                    Url.Combine(Provider.BaseUrl, OrderEndpoint.Orders),
                    Url.Combine(Provider.BaseUrl, OrderEndpoint.Orders, "*"))
                .WithVerb(HttpMethod.Post, HttpMethod.Get)
                .RespondWithJson(_orderReadDto);
        }

        private OrderReadDto CreateOrderReadDto()
        {
            return Fixture.Create<OrderReadDto>();
        }

        [Fact]
        public async Task CreateAsync_OrderIsValid_HttpRequestIsCreated()
        {
            OrderWriteDto orderWriteDto = CreateOrderWriteDto();

            OrderReadDto result = await Provider.CreateAsync(orderWriteDto);

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

        [Fact]
        public async Task GetByIdAsync_OrderIsValid_HttpRequestIsCreated()
        {
            string orderId = Guid.NewGuid().ToString();

            OrderReadDto result = await Provider.GetByIdAsync(orderId);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, OrderEndpoint.Orders, orderId))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Get)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(_orderReadDto);
        }
    }
}
