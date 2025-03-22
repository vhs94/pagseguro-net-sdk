using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Flurl;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.ChargedOrder;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Providers.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Orders
{
    public abstract class ChargedOrderProviderOfTests<TChargeWriteDto, TChargeReadDto>
        : BaseProviderTests<ChargedOrderProviderOf<TChargeWriteDto, TChargeReadDto>>
        where TChargeWriteDto : ChargeDto, new()
        where TChargeReadDto : ChargeDto
    {
        private ChargedOrderReadDto<TChargeReadDto> _orderReadDto = null!;
        private ChargedOrderWriteDto<TChargeWriteDto> _orderWriteDto = null!;

        public IMapper MapperMock { get; private set; } = null!;

        protected override void CreateMocks()
        {
            MapperMock = Substitute.For<IMapper>();
        }

        protected override void SetupMocks()
        {
            _orderReadDto = CreateOrderReadDto();
            _orderWriteDto = CreateOrderWriteDto();
            HttpTestMock
                .ForCallsTo(
                    Url.Combine(Provider.BaseUrl, OrderEndpoint.Orders),
                    Url.Combine(Provider.BaseUrl, OrderEndpoint.Orders, "*"))
                .WithVerb(HttpMethod.Post, HttpMethod.Get)
                .RespondWithJson(_orderReadDto);
            MapperMock
                .Map<ChargedOrderWriteDto<TChargeWriteDto>>(Arg.Any<OrderWriteDto>())
                .Returns(_orderWriteDto);
        }

        private ChargedOrderReadDto<TChargeReadDto> CreateOrderReadDto()
        {
            return Fixture.Create<ChargedOrderReadDto<TChargeReadDto>>();
        }

        private ChargedOrderWriteDto<TChargeWriteDto> CreateOrderWriteDto()
        {
            return Fixture.Create<ChargedOrderWriteDto<TChargeWriteDto>>();
        }

        [Fact]
        public void AddCharge_ChargeIsValid_ChargeIsSet()
        {
            TChargeWriteDto chargeWriteDto = CreateChargeWriteDto();

            Provider.AddCharge(chargeWriteDto);

            Provider.Build()
                .Charges
                .Should()
                .BeEquivalentTo([chargeWriteDto]);
        }

        private TChargeWriteDto CreateChargeWriteDto()
        {
            return Fixture.Create<TChargeWriteDto>();
        }

        [Fact]
        public void AddCharges_ChargeIsValid_ChargeIsSet()
        {
            TChargeWriteDto chargeWriteDto = CreateChargeWriteDto();
            var charges = new List<TChargeWriteDto>()
            {
                chargeWriteDto
            };

            Provider.AddCharges(charges);

            Provider.Build()
                .Charges
                .Should()
                .BeEquivalentTo(charges);
        }

        [Fact]
        public void Load_ByOrderWriteDto_MapperIsCalled()
        {
            string referenceId = "referenceId";
            var expectedOrderWriteDto = new OrderWriteDto
            {
                ReferenceId = referenceId
            };

            ChargedOrderWriteDto<TChargeWriteDto> orderWriteDto = Provider
                .Load(expectedOrderWriteDto)
                .Build();

            MapperMock
                .Received(1)
                .Map<ChargedOrderWriteDto<TChargeWriteDto>>(Arg.Is<OrderWriteDto>(
                    order => order.ReferenceId == referenceId));
            orderWriteDto
                .Should()
                .BeEquivalentTo(_orderWriteDto);
        }

        [Fact]
        public void Build_OrderIsReturned()
        {
            string referenceId = "referenceId";
            var expectedOrderWriteDto = new ChargedOrderWriteDto<TChargeWriteDto>
            {
                ReferenceId = referenceId
            };

            var orderWriteDto = Provider
                .Load(expectedOrderWriteDto)
                .Build();

            ChargedOrderWriteDto<TChargeWriteDto> secondOrderWriteDto = Provider
                .Build();
            orderWriteDto
                .Should()
                .BeEquivalentTo(expectedOrderWriteDto);
            secondOrderWriteDto
                .Should()
                .NotBeEquivalentTo(orderWriteDto);
        }

        [Fact]
        public async Task CreateAsync_OrderIsValid_HttpRequestIsCreated()
        {
            ChargedOrderReadDto<TChargeReadDto> result = await Provider.CreateAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, OrderEndpoint.Orders))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(Provider.Build())
                .Times(1);
            AssertChargeResponse(_orderReadDto, result);
        }

        protected virtual void AssertChargeResponse(
            ChargedOrderReadDto<TChargeReadDto> expectedReadDto,
            ChargedOrderReadDto<TChargeReadDto> receivedReadDto)
        {
            receivedReadDto
                .Should()
                .BeEquivalentTo(expectedReadDto);
        }

        [Fact]
        public async Task GetByIdAsync_OrderIsValid_HttpRequestIsCreated()
        {
            string orderId = Guid.NewGuid().ToString();

            ChargedOrderReadDto<TChargeReadDto> result = await Provider.GetByIdAsync(orderId);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, OrderEndpoint.Orders, orderId))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Get)
                .Times(1);
            AssertChargeResponse(_orderReadDto, result);
        }

        [Fact]
        public async Task PayAsync_OrderIsValid_HttpRequestIsCreated()
        {
            string orderId = Guid.NewGuid().ToString();

            ChargedOrderReadDto<TChargeReadDto> result = await Provider.PayAsync(orderId);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(
                    Provider.BaseUrl,
                    OrderEndpoint.Orders,
                    orderId,
                    OrderEndpoint.Pay))
                .WithOAuthBearerToken(Settings.Token)
                .WithRequestJson(new
                {
                    charges = Provider.Build().Charges
                })
                .WithVerb(HttpMethod.Post)
                .Times(1);
            AssertChargeResponse(_orderReadDto, result);
        }
    }
}
