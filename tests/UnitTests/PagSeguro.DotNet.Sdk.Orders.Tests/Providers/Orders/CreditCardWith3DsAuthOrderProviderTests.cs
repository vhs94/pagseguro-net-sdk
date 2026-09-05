using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Flurl;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Providers.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Orders
{
    public class CreditCardWith3DsAuthOrderProviderTests : BaseProviderTests<CreditCardWith3DsAuthOrderProvider>
    {
        private ChargedOrderResponse<ChargeByCreditCardWith3DsAuthResponse> _orderResponse = null!;
        private ChargedOrderRequest<ChargeByCreditCardWith3DsAuthRequest> _orderRequest = null!;

        public IMapper MapperMock { get; private set; } = null!;

        protected override void CreateMocks()
        {
            MapperMock = Substitute.For<IMapper>();
        }

        protected override CreditCardWith3DsAuthOrderProvider CreateProvider()
        {
            return new CreditCardWith3DsAuthOrderProvider(Settings, MapperMock, FlurlClientMock);
        }

        protected override void SetupMocks()
        {
            _orderResponse = CreateOrderResponse();
            _orderRequest = CreateOrderRequest();
            HttpTestMock
                .ForCallsTo(
                    Url.Combine(ProviderBaseUrl, OrderEndpoint.Orders),
                    Url.Combine(ProviderBaseUrl, OrderEndpoint.Orders, "*"))
                .WithVerb(HttpMethod.Post, HttpMethod.Get)
                .RespondWithJson(_orderResponse);
            MapperMock
                .Map<ChargedOrderRequest<ChargeByCreditCardWith3DsAuthRequest>>(Arg.Any<OrderRequest>())
                .Returns(_orderRequest);
        }

        private ChargedOrderResponse<ChargeByCreditCardWith3DsAuthResponse> CreateOrderResponse()
        {
            return Fixture.Create<ChargedOrderResponse<ChargeByCreditCardWith3DsAuthResponse>>();
        }

        private ChargedOrderRequest<ChargeByCreditCardWith3DsAuthRequest> CreateOrderRequest()
        {
            return Fixture.Create<ChargedOrderRequest<ChargeByCreditCardWith3DsAuthRequest>>();
        }

        [Fact]
        public void AddCharge_ChargeIsValid_ChargeIsSet()
        {
            ChargeByCreditCardWith3DsAuthRequest chargeRequest = CreateChargeRequest();

            Provider.AddCharge(chargeRequest);

            Provider.Build()
                .Charges
                .Should()
                .BeEquivalentTo([chargeRequest]);
        }

        private ChargeByCreditCardWith3DsAuthRequest CreateChargeRequest()
        {
            return Fixture.Create<ChargeByCreditCardWith3DsAuthRequest>();
        }

        [Fact]
        public void AddCharges_ChargeIsValid_ChargeIsSet()
        {
            ChargeByCreditCardWith3DsAuthRequest chargeRequest = CreateChargeRequest();
            var charges = new List<ChargeByCreditCardWith3DsAuthRequest>()
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

            ChargedOrderRequest<ChargeByCreditCardWith3DsAuthRequest> orderRequest = Provider
                .Load(expectedOrderRequest)
                .Build();

            MapperMock
                .Received(1)
                .Map<ChargedOrderRequest<ChargeByCreditCardWith3DsAuthRequest>>(Arg.Is<OrderRequest>(
                    order => order.ReferenceId == referenceId));
            orderRequest
                .Should()
                .BeEquivalentTo(_orderRequest);
        }

        [Fact]
        public void Build_OrderIsReturned()
        {
            string referenceId = "referenceId";
            var expectedOrderRequest = new ChargedOrderRequest<ChargeByCreditCardWith3DsAuthRequest>
            {
                ReferenceId = referenceId
            };

            var orderRequest = Provider
                .Load(expectedOrderRequest)
                .Build();

            ChargedOrderRequest<ChargeByCreditCardWith3DsAuthRequest> secondOrderRequest = Provider
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
            ChargedOrderResponse<ChargeByCreditCardWith3DsAuthResponse> result = await Provider.CreateAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(ProviderBaseUrl, OrderEndpoint.Orders))
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

            ChargedOrderResponse<ChargeByCreditCardWith3DsAuthResponse> result = await Provider.GetByIdAsync(orderId);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(ProviderBaseUrl, OrderEndpoint.Orders, orderId))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Get)
                .Times(1);
            result.Should().BeEquivalentTo(_orderResponse);
        }

        [Fact]
        public async Task PayAsync_OrderIsValid_HttpRequestIsCreated()
        {
            string orderId = Guid.NewGuid().ToString();

            ChargedOrderResponse<ChargeByCreditCardWith3DsAuthResponse> result = await Provider.PayAsync(orderId);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(
                    ProviderBaseUrl,
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
