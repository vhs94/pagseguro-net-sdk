using AutoFixture;
using FluentAssertions;
using Flurl;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Providers;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers
{
    public class ChargeProviderTests : BaseProviderTests<ChargeProvider>
    {
        private OrderReadDto _orderReadDto;
        private object _chargeReadDto;

        protected override ChargeProvider CreateProvider()
        {
            return new ChargeProvider(Settings);
        }

        protected override void SetupMocks()
        {
            _orderReadDto = CreateOrderReadDto();
            _chargeReadDto = CreateChargeReadDto();
            HttpTestMock
                .ForCallsTo(
                    Url.Combine(Provider.BaseUrl, OrderEndpoint.Orders),
                    Url.Combine(Provider.BaseUrl, OrderEndpoint.Orders, "*"))
                .WithVerb(HttpMethod.Post, HttpMethod.Get)
                .RespondWithJson(_orderReadDto);
            HttpTestMock
                .ForCallsTo(Url.Combine(Provider.BaseUrl, OrderEndpoint.Charges, "*"))
                .WithVerb(HttpMethod.Post, HttpMethod.Get)
                .RespondWithJson(_chargeReadDto);
        }

        private OrderReadDto CreateOrderReadDto()
        {
            return Fixture.Create<OrderReadDto>();
        }

        private ChargeReadDto CreateChargeReadDto()
        {
            return Fixture.Create<ChargeReadDto>();
        }

        [Fact]
        public async Task ChargeOrderAsync_OrderPayIsValid_HttpRequestIsCreated()
        {
            ChargeOrderDto chargeOrderDto = CreateChargeOrderDto();

            OrderReadDto result = await Provider.ChargeOrderAsync(chargeOrderDto);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(
                    Provider.BaseUrl,
                    OrderEndpoint.Orders,
                    chargeOrderDto.OrderId,
                    OrderEndpoint.Pay))
                .WithOAuthBearerToken(Settings.Token)
                .WithRequestJson(new
                {
                    charges = chargeOrderDto.Charges
                })
                .WithVerb(HttpMethod.Post)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(_orderReadDto);
        }

        private ChargeOrderDto CreateChargeOrderDto()
        {
            return Fixture.Create<ChargeOrderDto>();
        }

        [Fact]
        public async Task GetOrderChargeByIdAsync_IdIsValid_HttpRequestIsCreated()
        {
            string chargeId = "char_id";

            ChargeReadDto result = await Provider.GetOrderChargeByIdAsync(chargeId);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, OrderEndpoint.Charges, chargeId))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Get)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(_chargeReadDto);
        }

        [Fact]
        public async Task CaptureChargeAsync_ChargeIsValid_HttpRequestIsCreated()
        {
            CaptureChargeDto captureChargeDto = CreateCaptureChargeDto();

            ChargeReadDto result = await Provider.CaptureChargeAsync(captureChargeDto);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(
                    Provider.BaseUrl,
                    OrderEndpoint.Charges,
                    captureChargeDto.ChargeId,
                    OrderEndpoint.Capture))
                .WithOAuthBearerToken(Settings.Token)
                .WithRequestJson(new
                {
                    amount = new
                    {
                        value = captureChargeDto.AmountValue
                    }
                })
                .WithVerb(HttpMethod.Post)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(_chargeReadDto);
        }

        private CaptureChargeDto CreateCaptureChargeDto()
        {
            return Fixture.Create<CaptureChargeDto>();
        }

        [Fact]
        public async Task CancelChargeAsync_ChargeIsValid_HttpRequestIsCreated()
        {
            CancelChargeDto cancelChargeDto = CreateCancelChargeDto();

            ChargeReadDto result = await Provider.CancelChargeAsync(cancelChargeDto);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(
                    Provider.BaseUrl,
                    OrderEndpoint.Charges,
                    cancelChargeDto.ChargeId,
                    OrderEndpoint.Cancel))
                .WithOAuthBearerToken(Settings.Token)
                .WithRequestJson(new
                {
                    amount = new
                    {
                        value = cancelChargeDto.AmountValue
                    }
                })
                .WithVerb(HttpMethod.Post)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(_chargeReadDto);
        }

        private CancelChargeDto CreateCancelChargeDto()
        {
            return Fixture.Create<CancelChargeDto>();
        }
    }
}
