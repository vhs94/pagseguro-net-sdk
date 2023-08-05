using AutoFixture;
using FluentAssertions;
using Flurl;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;
using PagSeguro.DotNet.Sdk.Orders.Providers.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Orders
{
    public class OrderProviderTests : BaseProviderTests<OrderProvider>
    {
        private IServiceProvider _serviceProvider;
        private OrderReadDto _orderReadDto;

        protected override void CreateMocks()
        {
            _serviceProvider = Substitute.For<IServiceProvider>();
        }

        protected override OrderProvider CreateProvider()
        {
            return new OrderProvider(Settings, _serviceProvider);
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
        public void WithBankSlip_GetServiceIBankSlipOrderProviderIsCalled()
        {
            Provider.WithBankSlip();

            _serviceProvider
                .Received(1)
                .GetService<IBankSlipOrderProvider>();
        }

        [Fact]
        public void WithCreditCard_GetServiceICreditCardOrderProviderIsCalled()
        {
            Provider.WithCreditCard();

            _serviceProvider
                .Received(1)

                .GetService<ICreditCardOrderProvider>();
        }

        [Fact]
        public void WithCreditCardAnd3DsAuthentication_GetServiceICreditCardWith3DsAuthOrderProviderIsCalled()
        {
            Provider.WithCreditCardAnd3DsAuthentication();

            _serviceProvider
                .Received(1)
                .GetService<ICreditCardWith3DsAuthOrderProvider>();
        }

        [Fact]
        public void WithDebitCardAnd3DsAuthentication_GetServiceIDebitCardWith3DsAuthOrderProviderIsCalled()
        {
            Provider.WithDebitCardAnd3DsAuthentication();

            _serviceProvider
                .Received(1)
                .GetService<IDebitCardWith3DsAuthOrderProvider>();
        }

        [Fact]
        public async Task CreateAsync_OrderIsValid_HttpRequestIsCreated()
        {
            OrderWriteDto orderWriteDto = CreateOrderWriteDto();

            var result = await Provider.CreateAsync(orderWriteDto);

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
