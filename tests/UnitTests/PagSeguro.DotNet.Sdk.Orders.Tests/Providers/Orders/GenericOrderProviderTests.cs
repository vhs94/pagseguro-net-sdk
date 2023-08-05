using AutoFixture;
using FluentAssertions;
using Flurl;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.ChargedOrder;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Item;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.QrCode;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Shipping;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Providers.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Orders
{
    public abstract class GenericOrderProviderTests<TChargeWriteDto, TChargeReadDto>
        : BaseProviderTests<GenericOrderProvider<TChargeWriteDto, TChargeReadDto>>
        where TChargeWriteDto : ChargeDto, new()
        where TChargeReadDto : ChargeDto
    {
        private ChargedOrderReadDto<TChargeReadDto> _orderReadDto;

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

        private ChargedOrderReadDto<TChargeReadDto> CreateOrderReadDto()
        {
            return Fixture.Create<ChargedOrderReadDto<TChargeReadDto>>();
        }

        [Fact]
        public void AddCharge_ChargeIsValid_ChargeIsSet()
        {
            TChargeWriteDto chargeWriteDto = CreateChargeWriteDto();

            Provider.AddCharge(chargeWriteDto);

            Provider.Build()
                .Charges
                .Should()
                .BeEquivalentTo(new List<TChargeWriteDto>() { chargeWriteDto });
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
                .BeEquivalentTo(new List<ItemWriteDto>() { itemWriteDto });
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
                .BeEquivalentTo(new List<ItemWriteDto>() { itemWriteDto });
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
                .BeEquivalentTo(new List<QrCodeWriteDto>() { qrCodeWriteDto });
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

            ChargedOrderWriteDto<TChargeWriteDto> orderWriteDto = Provider
                .WithReferenceId(referenceId)
                .Build();

            ChargedOrderWriteDto<TChargeWriteDto> secondOrderWriteDto = Provider
                .Build();
            orderWriteDto
                .ReferenceId
                .Should().Be(referenceId);
            secondOrderWriteDto
                .ReferenceId
                .Should().NotBe(referenceId);
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
