using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Flurl;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;
using PagSeguro.DotNet.Sdk.Orders.Providers.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Orders
{
    public class DebitCardWith3DsAuthOrderProviderTests : BaseProviderTests<IDebitCardWith3DsAuthOrderProvider>
    {
        private ChargedOrderResponse<ChargeByDebitCardWith3DsAuthResponse> _orderResponse = null!;
        private ChargedOrderRequest<ChargeByDebitCardWith3DsAuthRequest> _orderRequest = null!;

        public IMapper MapperMock { get; private set; } = null!;

        protected override void CreateMocks()
        {
            MapperMock = Substitute.For<IMapper>();
        }

        protected override IDebitCardWith3DsAuthOrderProvider CreateProvider()
        {
            return new DebitCardWith3DsAuthOrderProvider(Settings, MapperMock, FlurlClientMock);
        }

        protected override void SetupMocks()
        {
            _orderResponse = CreateOrderResponse();
            _orderRequest = CreateOrderRequest();
            HttpTestMock
                .ForCallsTo(
                    Url.Combine(Provider.BaseUrl, OrderEndpoint.Orders),
                    Url.Combine(Provider.BaseUrl, OrderEndpoint.Orders, "*"))
                .WithVerb(HttpMethod.Post, HttpMethod.Get)
                .RespondWithJson(_orderResponse);
            MapperMock
                .Map<ChargedOrderRequest<ChargeByDebitCardWith3DsAuthRequest>>(Arg.Any<OrderRequest>())
                .Returns(_orderRequest);
        }

        private ChargedOrderResponse<ChargeByDebitCardWith3DsAuthResponse> CreateOrderResponse()
        {
            return Fixture.Create<ChargedOrderResponse<ChargeByDebitCardWith3DsAuthResponse>>();
        }

        private ChargedOrderRequest<ChargeByDebitCardWith3DsAuthRequest> CreateOrderRequest()
        {
            return Fixture.Create<ChargedOrderRequest<ChargeByDebitCardWith3DsAuthRequest>>();
        }

        [Fact]
        public void AddCharge_ChargeIsValid_ChargeIsSet()
        {
            ChargeByDebitCardWith3DsAuthRequest chargeRequest = CreateChargeRequest();

            Provider.AddCharge(chargeRequest);

            Provider.Build()
                .Charges
                .Should()
                .BeEquivalentTo([chargeRequest]);
        }

        private ChargeByDebitCardWith3DsAuthRequest CreateChargeRequest()
        {
            return Fixture.Create<ChargeByDebitCardWith3DsAuthRequest>();
        }

        [Fact]
        public void AddCharges_ChargeIsValid_ChargeIsSet()
        {
            ChargeByDebitCardWith3DsAuthRequest chargeRequest = CreateChargeRequest();
            var charges = new List<ChargeByDebitCardWith3DsAuthRequest>()
            {
                chargeRequest
            };

            Provider.AddCharges(charges);

            Provider.Build()
                .Charges
                .Should()
                .BeEquivalentTo(charges);
        }

        [Fact]
        public void Load_ByOrderRequest_MapperIsCalled()
        {
            string referenceId = "referenceId";
            var expectedOrderRequest = new OrderRequest
            {
                ReferenceId = referenceId
            };

            ChargedOrderRequest<ChargeByDebitCardWith3DsAuthRequest> orderRequest = Provider
                .Load(expectedOrderRequest)
                .Build();

            MapperMock
                .Received(1)
                .Map<ChargedOrderRequest<ChargeByDebitCardWith3DsAuthRequest>>(Arg.Is<OrderRequest>(
                    order => order.ReferenceId == referenceId));
            orderRequest
                .Should()
                .BeEquivalentTo(_orderRequest);
        }

        [Fact]
        public void Build_OrderIsReturned()
        {
            string referenceId = "referenceId";
            var expectedOrderRequest = new ChargedOrderRequest<ChargeByDebitCardWith3DsAuthRequest>
            {
                ReferenceId = referenceId
            };

            var orderRequest = Provider
                .Load(expectedOrderRequest)
                .Build();

            ChargedOrderRequest<ChargeByDebitCardWith3DsAuthRequest> secondOrderRequest = Provider
                .Build();
            orderRequest
                .Should()
                .BeEquivalentTo(expectedOrderRequest);
            secondOrderRequest
                .Should()
                .NotBeEquivalentTo(orderRequest);
        }

        [Fact]
        public async Task CreateAsync_OrderIsValid_HttpRequestIsCreated()
        {
            ChargedOrderResponse<ChargeByDebitCardWith3DsAuthResponse> result = await Provider.CreateAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, OrderEndpoint.Orders))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(Provider.Build())
                .Times(1);
            result.Should().BeEquivalentTo(_orderResponse);
        }

        [Fact]
        public async Task GetByIdAsync_OrderIsValid_HttpRequestIsCreated()
        {
            string orderId = Guid.NewGuid().ToString();

            ChargedOrderResponse<ChargeByDebitCardWith3DsAuthResponse> result = await Provider.GetByIdAsync(orderId);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, OrderEndpoint.Orders, orderId))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Get)
                .Times(1);
            result.Should().BeEquivalentTo(_orderResponse);
        }

        [Fact]
        public async Task PayAsync_OrderIsValid_HttpRequestIsCreated()
        {
            string orderId = Guid.NewGuid().ToString();

            ChargedOrderResponse<ChargeByDebitCardWith3DsAuthResponse> result = await Provider.PayAsync(orderId);

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
            result.Should().BeEquivalentTo(_orderResponse);
        }
    }
}
