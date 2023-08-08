using AutoFixture;
using FluentAssertions;
using Flurl;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Providers.Charges;


namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Charges
{
    public abstract class GenericChargeProviderTests<TChargeWriteDto, TChargeReadDto>
        : BaseProviderTests<GenericChargeProvider<TChargeWriteDto, TChargeReadDto>>
        where TChargeWriteDto : ChargeDto, new()
        where TChargeReadDto : ChargeDto
    {
        private TChargeReadDto _chargeReadDto;

        protected override void SetupMocks()
        {
            _chargeReadDto = CreateChargeReadDto();
            HttpTestMock
                .ForCallsTo(
                    Url.Combine(Provider.BaseUrl, OrderEndpoint.Charges),
                    Url.Combine(Provider.BaseUrl, OrderEndpoint.Charges, "*"))
                .WithVerb(HttpMethod.Post, HttpMethod.Get)
                .RespondWithJson(_chargeReadDto);
        }

        private TChargeReadDto CreateChargeReadDto()
        {
            return Fixture.Create<TChargeReadDto>();
        }

        [Fact]
        public void WithDescription_DescriptionIsValid_ChargeDescriptionIsSet()
        {
            string description = "description";

            Provider.WithDescription(description);

            Provider.Build()
                .Description
                .Should()
                .Be(description);
        }

        [Fact]
        public void WithNotificationUrl_NotificationUrlIsValid_NotificationUrlIsSet()
        {
            string firstItem = Guid.NewGuid().ToString();
            string secondItem = Guid.NewGuid().ToString();

            Provider.WithNotificationUrl(firstItem);
            Provider.WithNotificationUrl(secondItem);

            Provider.Build()
                .NotificationUrls
                .Should()
                .BeEquivalentTo(new List<string>
                {
                    firstItem,
                    secondItem
                });
        }

        [Fact]
        public void WithNotificationUrls_NotificationUrlsAreValid_NotificationUrlsAreSet()
        {
            string firstItem = Guid.NewGuid().ToString();
            string secondItem = Guid.NewGuid().ToString();
            var notificationUrls = new List<string>
            {
                firstItem,
                secondItem
            };

            Provider.WithNotificationUrls(notificationUrls);

            Provider.Build()
                .NotificationUrls
                .Should()
                .BeEquivalentTo(notificationUrls);
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
        public void Load_ChargeIsLoaded()
        {
            string referenceId = "referenceId";
            var expectedChargeWrite = new TChargeWriteDto
            {
                ReferenceId = referenceId
            };

            TChargeWriteDto chargeWriteDto = Provider
                .Load(expectedChargeWrite)
                .Build();

            chargeWriteDto
                .Should()
                .BeEquivalentTo(expectedChargeWrite);
        }

        [Fact]
        public void Build_ChargeIsReturned()
        {
            string referenceId = "referenceId";

            TChargeWriteDto chargeWriteDto = Provider
                .WithReferenceId(referenceId)
                .Build();

            TChargeWriteDto secondChargeWriteDto = Provider
                .Build();
            chargeWriteDto
                .ReferenceId
                .Should().Be(referenceId);
            secondChargeWriteDto
                .ReferenceId
                .Should().NotBe(referenceId);
        }

        [Fact]
        public async Task ChargeAsync_ChargeIsValid_HttpRequestIsCreated()
        {
            TChargeReadDto result = await Provider.ChargeAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(Provider.BaseUrl, OrderEndpoint.Charges))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey)
                .WithRequestJson(Provider.Build())
                .WithVerb(HttpMethod.Post)
                .Times(1);
            AssertChargeResponse(_chargeReadDto, result);
        }

        protected virtual void AssertChargeResponse(
            TChargeReadDto expectedReadDto,
            TChargeReadDto receivedReadDto)
        {
            receivedReadDto
                .Should()
                .BeEquivalentTo(expectedReadDto);
        }

        [Fact]
        public async Task GetByIdAsync_ChargeIsValid_HttpRequestIsCreated()
        {
            string chargeId = Guid.NewGuid().ToString();

            TChargeReadDto result = await Provider.GetByIdAsync(chargeId);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(
                    Provider.BaseUrl,
                    OrderEndpoint.Charges,
                    chargeId))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Get)
                .Times(1);
            AssertChargeResponse(_chargeReadDto, result);
        }

        [Fact]
        public async Task CancelAsync_ChargeIsValid_HttpRequestIsCreated()
        {
            CancelChargeDto cancelChargeDto = CreateCancelChargeDto();

            TChargeReadDto result = await Provider.CancelAsync(cancelChargeDto);

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
            AssertChargeResponse(_chargeReadDto, result);
        }

        private CancelChargeDto CreateCancelChargeDto()
        {
            return Fixture.Create<CancelChargeDto>();
        }

        [Fact]
        public async Task CaptureAsync_ChargeIsValid_HttpRequestIsCreated()
        {
            string chargeId = Guid.NewGuid().ToString();

            TChargeReadDto result = await Provider
                .WithId(chargeId)
                .CaptureAsync(100);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(
                    Provider.BaseUrl,
                    OrderEndpoint.Charges,
                    chargeId,
                    OrderEndpoint.Capture))
                .WithOAuthBearerToken(Settings.Token)
                .WithRequestJson(new
                {
                    amount = new
                    {
                        value = 100
                    }
                })
                .WithVerb(HttpMethod.Post)
                .Times(1);
            AssertChargeResponse(_chargeReadDto, result);
        }

        protected ChargeAmountWriteDto CreateChargeAmountWriteDto()
        {
            return Fixture.Create<ChargeAmountWriteDto>();
        }

        protected IDictionary<string, string> CreateMetadata()
        {
            return Fixture.Create<IDictionary<string, string>>();
        }
    }
}
