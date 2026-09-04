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
    public class BankSlipOrderProviderTests : BaseProviderTests<IBankSlipOrderProvider>
    {
        private ChargedOrderResponse<ChargeByBankSlipResponse> _orderResponse = null!;
        private ChargedOrderRequest<ChargeByBankSlipRequest> _orderRequest = null!;

        public IMapper MapperMock { get; private set; } = null!;

        protected override void CreateMocks()
        {
            MapperMock = Substitute.For<IMapper>();
        }

        protected override IBankSlipOrderProvider CreateProvider()
        {
            return new BankSlipOrderProvider(Settings, MapperMock, FlurlClientMock);
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
                .Map<ChargedOrderRequest<ChargeByBankSlipRequest>>(Arg.Any<OrderRequest>())
                .Returns(_orderRequest);
        }

        private ChargedOrderResponse<ChargeByBankSlipResponse> CreateOrderResponse()
        {
            return Fixture.Create<ChargedOrderResponse<ChargeByBankSlipResponse>>();
        }

        private ChargedOrderRequest<ChargeByBankSlipRequest> CreateOrderRequest()
        {
            return Fixture.Create<ChargedOrderRequest<ChargeByBankSlipRequest>>();
        }

        [Fact]
        public void AddCharge_ChargeIsValid_ChargeIsSet()
        {
            ChargeByBankSlipRequest chargeRequest = CreateChargeRequest();

            Provider.AddCharge(chargeRequest);

            Provider.Build()
                .Charges
                .Should()
                .BeEquivalentTo([chargeRequest]);
        }

        private ChargeByBankSlipRequest CreateChargeRequest()
        {
            return Fixture.Create<ChargeByBankSlipRequest>();
        }

        [Fact]
        public void AddCharges_ChargeIsValid_ChargeIsSet()
        {
            ChargeByBankSlipRequest chargeRequest = CreateChargeRequest();
            var charges = new List<ChargeByBankSlipRequest>()
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

            ChargedOrderRequest<ChargeByBankSlipRequest> orderRequest = Provider
                .Load(expectedOrderRequest)
                .Build();

            MapperMock
                .Received(1)
                .Map<ChargedOrderRequest<ChargeByBankSlipRequest>>(Arg.Is<OrderRequest>(
                    order => order.ReferenceId == referenceId));
            orderRequest
                .Should()
                .BeEquivalentTo(_orderRequest);
        }

        [Fact]
        public void Build_OrderIsReturned()
        {
            string referenceId = "referenceId";
            var expectedOrderRequest = new ChargedOrderRequest<ChargeByBankSlipRequest>
            {
                ReferenceId = referenceId
            };

            var orderRequest = Provider
                .Load(expectedOrderRequest)
                .Build();

            ChargedOrderRequest<ChargeByBankSlipRequest> secondOrderRequest = Provider
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
            ChargedOrderResponse<ChargeByBankSlipResponse> result = await Provider.CreateAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, OrderEndpoint.Orders))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(Provider.Build())
                .Times(1);
            AssertChargeResponse(_orderResponse, result);
        }

        private void AssertChargeResponse(
            ChargedOrderResponse<ChargeByBankSlipResponse> expectedResponse,
            ChargedOrderResponse<ChargeByBankSlipResponse> receivedResponse)
        {
            receivedResponse
                .Should()
                .BeEquivalentTo(
                    expectedResponse,
                    options => options.Excluding(f => f.Charges));
            receivedResponse
                .Charges
                .Should()
                .BeEquivalentTo(expectedResponse.Charges,
                    options => options.Excluding(f => f.PaymentMethod!.BankSlip!.DueDate));
            receivedResponse
                .Charges
                .Select(cg => cg.PaymentMethod!.BankSlip!.DueDate)
                .Should()
                .BeEquivalentTo(expectedResponse.Charges.Select(cg => cg.PaymentMethod!.BankSlip!.DueDate.Date));
        }

        [Fact]
        public async Task GetByIdAsync_OrderIsValid_HttpRequestIsCreated()
        {
            string orderId = Guid.NewGuid().ToString();

            ChargedOrderResponse<ChargeByBankSlipResponse> result = await Provider.GetByIdAsync(orderId);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, OrderEndpoint.Orders, orderId))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Get)
                .Times(1);
            AssertChargeResponse(_orderResponse, result);
        }

        [Fact]
        public async Task PayAsync_OrderIsValid_HttpRequestIsCreated()
        {
            string orderId = Guid.NewGuid().ToString();

            ChargedOrderResponse<ChargeByBankSlipResponse> result = await Provider.PayAsync(orderId);

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
            AssertChargeResponse(_orderResponse, result);
        }
    }
}
