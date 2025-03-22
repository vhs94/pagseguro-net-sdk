using AutoFixture;
using FluentAssertions;
using Flurl;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByBankSlip;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.ChargedOrder;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Item;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.QrCode;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Shipping;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;
using PagSeguro.DotNet.Sdk.Orders.Providers.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Orders
{
    public class OrderProviderTests : BaseProviderTests<OrderProvider>
    {
        private IServiceProvider _serviceProviderMock = null!;
        private OrderReadDto _orderReadDto = null!;
        private OrderWriteDto _orderWriteDto = null!;
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
            return new OrderProvider(Settings, _serviceProviderMock);
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

        private OrderReadDto CreateOrderReadDto()
        {
            return Fixture.Create<OrderReadDto>();
        }

        private OrderWriteDto CreateOrderWriteDto()
        {
            return Fixture.Create<OrderWriteDto>();
        }

        [Fact]
        public void WithCustomer_CustomerIsValid_CustomerIsSet()
        {
            CustomerDto customerDto = CreateCustomerDto();

            Provider.WithCustomer(customerDto);

            Provider.Build()
                .Customer
                .Should()
                .BeEquivalentTo(customerDto);
        }

        private CustomerDto CreateCustomerDto()
        {
            return Fixture.Create<CustomerDto>();
        }

        [Fact]
        public void WithItem_ItemIsValid_ItemIsSet()
        {
            ItemWriteDto itemWriteDto = CreateItemWriteDto();

            Provider.WithItem(itemWriteDto);

            Provider.Build()
                .Items
                .Should()
                .BeEquivalentTo([itemWriteDto]);
        }

        private ItemWriteDto CreateItemWriteDto()
        {
            return Fixture.Create<ItemWriteDto>();
        }

        [Fact]
        public void WithItems_ItemIsValid_ItemIsSet()
        {
            ItemWriteDto itemWriteDto = CreateItemWriteDto();
            var items = new List<ItemWriteDto>()
            {
                itemWriteDto
            };

            Provider.WithItems(items);

            Provider.Build()
                .Items
                .Should()
                .BeEquivalentTo([itemWriteDto]);
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
            QrCodeWriteDto qrCodeWriteDto = CreateQrCodeWriteDto();

            Provider.WithQrCode(qrCodeWriteDto);

            Provider.Build()
                .QrCodes
                .Should()
                .BeEquivalentTo([qrCodeWriteDto]);
        }

        private QrCodeWriteDto CreateQrCodeWriteDto()
        {
            return Fixture.Create<QrCodeWriteDto>();
        }

        [Fact]
        public void WithQrCodes_QrCodeIsValid_QrCodeIsSet()
        {
            QrCodeWriteDto qrCodeWriteDto = CreateQrCodeWriteDto();
            var qrCodeWriteDtos = new List<QrCodeWriteDto>()
            {
                qrCodeWriteDto
            };

            Provider.WithQrCodes(qrCodeWriteDtos);

            Provider.Build()
                .QrCodes
                .Should()
                .BeEquivalentTo(qrCodeWriteDtos);
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
        public void WithShipping_ShippingIsValid_ShippingIdIsSet()
        {
            ShippingDto shippingDto = CreateShippingDto();

            Provider.WithShipping(shippingDto);

            Provider.Build()
                .Shipping
                .Should()
                .BeEquivalentTo(shippingDto);
        }

        private ShippingDto CreateShippingDto()
        {
            return Fixture.Create<ShippingDto>();
        }

        [Fact]
        public void Build_OrderIsReturned()
        {
            string referenceId = "referenceId";
            var expectedOrderWriteDto = new OrderWriteDto
            {
                ReferenceId = referenceId
            };

            var orderWriteDto = Provider
                .Load(expectedOrderWriteDto)
                .Build();

            var secondOrderWriteDto = Provider
                .Build();
            orderWriteDto
                .Should()
                .BeEquivalentTo(expectedOrderWriteDto);
            secondOrderWriteDto
                .Should()
                .NotBeEquivalentTo(orderWriteDto);
        }

        [Fact]
        public void WithBankSlip_ChargedOrderIsLoaded()
        {
            ChargedOrderWriteDto<ChargeByBankSlipWriteDto> chargedOrderWriteDto = Provider
                .Load(_orderWriteDto)
                .WithBankSlip()
                .Build();

            _serviceProviderMock
                .Received(1)
                .GetService<IBankSlipOrderProvider>();
            _bankSlipOrderProviderMock
                .Received(1)
                .Load(Arg.Is<OrderWriteDto>(order => AssertLoadedOrder(order)));
        }

        private bool AssertLoadedOrder(OrderWriteDto orderWriteDto)
        {
            orderWriteDto
                .Should()
                .BeEquivalentTo(_orderWriteDto);
            return true;
        }

        [Fact]
        public void WithCreditCard_ChargedOrderIsLoaded()
        {
            ChargedOrderWriteDto<ChargeByCreditCardWriteDto> chargedOrderWriteDto = Provider
                .Load(_orderWriteDto)
                .WithCreditCard()
                .Build();

            _serviceProviderMock
                .Received(1)
                .GetService<ICreditCardOrderProvider>();
            _creditCardOrderProviderMock
                .Received(1)
                .Load(Arg.Is<OrderWriteDto>(order => AssertLoadedOrder(order)));
        }

        [Fact]
        public void WithCreditCardAnd3DsAuthentication_ChargedOrderIsLoaded()
        {
            ChargedOrderWriteDto<ChargeByCreditCardWith3DsAuthWriteDto> chargedOrderWriteDto = Provider
                .Load(_orderWriteDto)
                .WithCreditCardAnd3DsAuthentication()
                .Build();

            _serviceProviderMock
                .Received(1)
                .GetService<ICreditCardWith3DsAuthOrderProvider>();
            _creditCardWith3DsAuthOrderProviderMock
                .Received(1)
                .Load(Arg.Is<OrderWriteDto>(order => AssertLoadedOrder(order)));
        }

        [Fact]
        public void WithDebitCardAnd3DsAuthentication_ChargedOrderIsLoaded()
        {
            var chargedOrderWriteDto = Provider
                .Load(_orderWriteDto)
                .WithDebitCardAnd3DsAuthentication()
                .Build();

            _serviceProviderMock
                .Received(1)
                .GetService<IDebitCardWith3DsAuthOrderProvider>();
            _debitCardWith3DsAuthOrderProviderMock
                .Received(1)
                .Load(Arg.Is<OrderWriteDto>(order => AssertLoadedOrder(order)));
        }

        [Fact]
        public async Task CreateAsync_OrderIsValid_HttpRequestIsCreated()
        {
            var result = await Provider
                .Load(_orderWriteDto)
                .CreateAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, OrderEndpoint.Orders))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(_orderWriteDto)
                .Times(1);
            result
                .Should()
                .BeEquivalentTo(_orderReadDto);
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
