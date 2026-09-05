using AutoFixture;
using FluentAssertions;
using Flurl;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Models.Shared;
using PagSeguro.DotNet.Sdk.Orders.Providers.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Orders
{
    public class OrderProviderTests : BaseProviderTests<OrderProvider>
    {
        private IServiceProvider _serviceProviderMock = null!;
        private OrderResponse _orderResponse = null!;
        private OrderRequest _orderRequest = null!;
        private IBankSlipOrderProvider _bankSlipOrderProviderMock = null!;
        private ICreditCardOrderProvider _creditCardOrderProviderMock = null!;
        private ICreditCardWith3DsAuthOrderProvider _creditCardWith3DsAuthOrderProviderMock = null!;
        private IDebitCardWith3DsAuthOrderProvider _debitCardWith3DsAuthOrderProviderMock = null!;

        protected override void CreateMocks()
        {
            _serviceProviderMock = Substitute.For<IServiceProvider>();
            Substitute.For<IServiceProvider>();
            _bankSlipOrderProviderMock = Substitute.For<IBankSlipOrderProvider>();
            _creditCardOrderProviderMock = Substitute.For<ICreditCardOrderProvider>();
            _creditCardWith3DsAuthOrderProviderMock = Substitute.For<ICreditCardWith3DsAuthOrderProvider>();
            _debitCardWith3DsAuthOrderProviderMock = Substitute.For<IDebitCardWith3DsAuthOrderProvider>();

        }

        protected override OrderProvider CreateProvider()
        {
            return new OrderProvider(Settings, _serviceProviderMock, FlurlClientMock);
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
            _serviceProviderMock
                .GetService<IBankSlipOrderProvider>()
                .Returns(_bankSlipOrderProviderMock);
            _serviceProviderMock
                .GetService<ICreditCardOrderProvider>()
                .Returns(_creditCardOrderProviderMock);
            _serviceProviderMock
                .GetService<ICreditCardWith3DsAuthOrderProvider>()
                .Returns(_creditCardWith3DsAuthOrderProviderMock);
            _serviceProviderMock
                .GetService<IDebitCardWith3DsAuthOrderProvider>()
                .Returns(_debitCardWith3DsAuthOrderProviderMock);
        }

        private OrderResponse CreateOrderResponse()
        {
            return Fixture.Create<OrderResponse>();
        }

        private OrderRequest CreateOrderRequest()
        {
            return Fixture.Create<OrderRequest>();
        }

        [Fact]
        public void WithCustomer_CustomerIsValid_CustomerIsSet()
        {
            Customer customer = CreateCustomer();

            Provider.WithCustomer(customer);

            Provider.Build()
                .Customer
                .Should()
                .BeEquivalentTo(customer);
        }

        private Customer CreateCustomer()
        {
            return Fixture.Create<Customer>();
        }

        [Fact]
        public void WithItem_ItemIsValid_ItemIsSet()
        {
            ItemRequest itemRequest = CreateItemRequest();

            Provider.WithItem(itemRequest);

            Provider.Build()
                .Items
                .Should()
                .BeEquivalentTo([itemRequest]);
        }

        private ItemRequest CreateItemRequest()
        {
            return Fixture.Create<ItemRequest>();
        }

        [Fact]
        public void WithItems_ItemIsValid_ItemIsSet()
        {
            ItemRequest itemRequest = CreateItemRequest();
            var items = new List<ItemRequest>()
            {
                itemRequest
            };

            Provider.WithItems(items);

            Provider.Build()
                .Items
                .Should()
                .BeEquivalentTo([itemRequest]);
        }

        [Fact]
        public void WithNotificationUrl_UrlIsValid_UrlIsSet()
        {
            string notificationUrl = "http://google.com";

            Provider.WithNotificationUrl(notificationUrl);

            Provider.Build()
                .NotificationUrls
                .Should()
                .BeEquivalentTo(new List<string>() { notificationUrl });
        }

        [Fact]
        public void WithNotificationUrls_UrlIsValid_UrlIsSet()
        {
            string notificationUrl = "http://google.com";
            var notificationUrls = new List<string>()
            {
                notificationUrl
            };

            Provider.WithNotificationUrls(notificationUrls);

            Provider.Build()
                .NotificationUrls
                .Should()
                .BeEquivalentTo(notificationUrls);
        }

        [Fact]
        public void WithQrCode_QrCodeIsValid_QrCodeIsSet()
        {
            QrCodeRequest qrCodeRequest = CreateQrCodeRequest();

            Provider.WithQrCode(qrCodeRequest);

            Provider.Build()
                .QrCodes
                .Should()
                .BeEquivalentTo([qrCodeRequest]);
        }

        private QrCodeRequest CreateQrCodeRequest()
        {
            return Fixture.Create<QrCodeRequest>();
        }

        [Fact]
        public void WithQrCodes_QrCodeIsValid_QrCodeIsSet()
        {
            QrCodeRequest qrCodeRequest = CreateQrCodeRequest();
            var qrCodeRequests = new List<QrCodeRequest>()
            {
                qrCodeRequest
            };

            Provider.WithQrCodes(qrCodeRequests);

            Provider.Build()
                .QrCodes
                .Should()
                .BeEquivalentTo(qrCodeRequests);
        }

        [Fact]
        public void WithReferenceId_ReferenceIdIsValid_ReferenceIdIsSet()
        {
            string referenceId = "referenceId";

            Provider.WithReferenceId(referenceId);

            Provider.Build()
                .ReferenceId
                .Should()
                .Be(referenceId);
        }

        [Fact]
        public void WithShipping_ShippingIsValid_ShippingIsSet()
        {
            Shipping shipping = Fixture.Create<Shipping>();

            Provider.WithShipping(shipping);

            Provider.Build()
                .Shipping
                .Should()
                .BeEquivalentTo(shipping);
        }

        [Fact]
        public void WithBankSlip_ChargedOrderIsLoaded()
        {
            ChargedOrderRequest<ChargeByBankSlipRequest> chargedOrderRequest = Provider
                .Load(_orderRequest)
                .WithBankSlip()
                .Build();

            _serviceProviderMock
                .Received(1)
                .GetService<IBankSlipOrderProvider>();
            _bankSlipOrderProviderMock
                .Received(1)
                .Load(Arg.Is<OrderRequest>(order => AssertLoadedOrder(order)));
        }

        private bool AssertLoadedOrder(OrderRequest orderRequest)
        {
            orderRequest
                .Should()
                .BeEquivalentTo(_orderRequest);
            return true;
        }

        [Fact]
        public void WithCreditCard_ChargedOrderIsLoaded()
        {
            ChargedOrderRequest<ChargeByCreditCardRequest> chargedOrderRequest = Provider
                .Load(_orderRequest)
                .WithCreditCard()
                .Build();

            _serviceProviderMock
                .Received(1)
                .GetService<ICreditCardOrderProvider>();
            _creditCardOrderProviderMock
                .Received(1)
                .Load(Arg.Is<OrderRequest>(order => AssertLoadedOrder(order)));
        }

        [Fact]
        public void WithCreditCardAnd3DsAuthentication_ChargedOrderIsLoaded()
        {
            ChargedOrderRequest<ChargeByCreditCardWith3DsAuthRequest> chargedOrderRequest = Provider
                .Load(_orderRequest)
                .WithCreditCardAnd3DsAuthentication()
                .Build();

            _serviceProviderMock
                .Received(1)
                .GetService<ICreditCardWith3DsAuthOrderProvider>();
            _creditCardWith3DsAuthOrderProviderMock
                .Received(1)
                .Load(Arg.Is<OrderRequest>(order => AssertLoadedOrder(order)));
        }

        [Fact]
        public void WithDebitCardAnd3DsAuthentication_ChargedOrderIsLoaded()
        {
            var chargedOrderRequest = Provider
                .Load(_orderRequest)
                .WithDebitCardAnd3DsAuthentication()
                .Build();

            _serviceProviderMock
                .Received(1)
                .GetService<IDebitCardWith3DsAuthOrderProvider>();
            _debitCardWith3DsAuthOrderProviderMock
                .Received(1)
                .Load(Arg.Is<OrderRequest>(order => AssertLoadedOrder(order)));
        }

        [Fact]
        public async Task CreateAsync_OrderIsValid_HttpRequestIsCreated()
        {
            var result = await Provider
                .Load(_orderRequest)
                .CreateAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(ProviderBaseUrl, OrderEndpoint.Orders))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(_orderRequest)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(_orderResponse);
        }

        [Fact]
        public async Task CreateAsync_OrderHasQrCodes_QrCodesAreSentInThePayload()
        {
            OrderRequest orderRequest = Fixture.Build<OrderRequest>()
                .With(o => o.QrCodes, [Fixture.Create<QrCodeRequest>()])
                .With(o => o.Items, [Fixture.Create<ItemRequest>()])
                .Create();

            await Provider.Load(orderRequest).CreateAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(ProviderBaseUrl, OrderEndpoint.Orders))
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(orderRequest)
                .Times(1);
        }

        [Fact]
        public async Task CreateAsync_OrderHasNoQrCodes_QrCodesAreOmittedFromThePayload()
        {
            // Regressão: a API recusa qr_codes: [] com 40002 "must have at least 1
            // element", o que quebrava todo pedido pago por cartão ou boleto.
            OrderRequest orderRequest = Fixture.Build<OrderRequest>()
                .Without(o => o.QrCodes)
                .With(o => o.Items, [Fixture.Create<ItemRequest>()])
                .Create();

            await Provider.Load(orderRequest).CreateAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(ProviderBaseUrl, OrderEndpoint.Orders))
                .WithVerb(HttpMethod.Post)
                .With(call => !call.RequestBody.Contains("qr_codes"))
                .Times(1);
        }

        [Fact]
        public async Task GetByIdAsync_OrderIsValid_HttpRequestIsCreated()
        {
            string orderId = Guid.NewGuid().ToString();

            OrderResponse result = await Provider.GetByIdAsync(orderId);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(ProviderBaseUrl, OrderEndpoint.Orders, orderId))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Get)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(_orderResponse);
        }
    }
}
