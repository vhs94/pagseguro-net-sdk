using AutoFixture;
using FluentAssertions;
using Flurl;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Charges
{
    public abstract class BaseChargeProviderTests<TTopLevelProvider, TChargeWriteDto, TChargeReadDto>
        : BaseProviderTests<IChargeProviderOf<TTopLevelProvider, TChargeWriteDto, TChargeReadDto>>
        where TChargeWriteDto : ChargeDto, IChargeWriteDto, new()
        where TChargeReadDto : ChargeDto
        where TTopLevelProvider : IChargeProvider<TChargeWriteDto, TChargeReadDto>
    {
        public TChargeReadDto ChargeReadDto { get; private set; } = null!;
        public TTopLevelProvider TopLevelProviderMock => (TTopLevelProvider)Provider;

        protected override void SetupMocks()
        {
            ChargeReadDto = CreateChargeReadDto();
            HttpTestMock
                .ForCallsTo(
                    Url.Combine(Provider.BaseUrl, OrderEndpoint.Charges),
                    Url.Combine(Provider.BaseUrl, OrderEndpoint.Charges, "*"))
                .WithVerb(HttpMethod.Post, HttpMethod.Get)
                .RespondWithJson(ChargeReadDto);
        }

        private TChargeReadDto CreateChargeReadDto()
        {
            return Fixture.Create<TChargeReadDto>();
        }

        [Fact]
        public void WithAmount_AmountIsValid_AmountIsSet()
        {
            ChargeAmountWriteDto chargeAmountWriteDto = CreateChargeAmountWriteDto();

            Provider.WithAmount(chargeAmountWriteDto);

            Provider
                .Build()
                .Amount
                .Should().BeEquivalentTo(chargeAmountWriteDto);
        }

        protected ChargeAmountWriteDto CreateChargeAmountWriteDto()
        {
            return Fixture.Create<ChargeAmountWriteDto>();
        }

        [Fact]
        public void WithDescription_DescriptionIsValid_ChargeDescriptionIsSet()
        {
            string description = "description";

            Provider.WithDescription(description);

            Provider.Build().Description.Should().Be(description);
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

            Provider.Build().NotificationUrls.Should().BeEquivalentTo(notificationUrls);
        }

        [Fact]
        public void WithReferenceId_ReferenceIdIsValid_ReferenceIdIsSet()
        {
            string referenceId = "referenceId";

            Provider.WithReferenceId(referenceId);

            Provider.Build().ReferenceId.Should().Be(referenceId);
        }

        [Fact]
        public void Load_ChargeIsLoaded()
        {
            string referenceId = "referenceId";
            var expectedChargeWrite = new TChargeWriteDto
            {
                ReferenceId = referenceId
            };

            Provider.Load(expectedChargeWrite);

            TopLevelProviderMock
                .ChargeWriteDto
                .Should()
                .BeEquivalentTo(expectedChargeWrite);
        }

        [Fact]
        public void Build_ChargeIsReturned()
        {
            string referenceId = "referenceId";
            var expectedChargeWrite = new TChargeWriteDto
            {
                ReferenceId = referenceId
            };

            Provider.WithReferenceId(referenceId);
            TChargeWriteDto chargeWriteDto = Provider.Build();

            TChargeWriteDto secondChargeWriteDto = Provider.Build();
            chargeWriteDto.Should().BeEquivalentTo(expectedChargeWrite);
            secondChargeWriteDto.Should().NotBeEquivalentTo(expectedChargeWrite);
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
            AssertChargeReadDto(result);
        }

        protected virtual void AssertChargeReadDto(TChargeReadDto receivedChargeReadDto)
        {
            receivedChargeReadDto
                .Should()
                .BeEquivalentTo(ChargeReadDto);
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
            AssertChargeReadDto(result);
        }

        [Fact]
        public async Task CancelAsync_ChargeIsValid_HttpRequestIsCreated()
        {
            string chargeId = Guid.NewGuid().ToString();

            TChargeReadDto result = await Provider.WithId(chargeId).CancelAsync(100);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(
                    Provider.BaseUrl,
                    OrderEndpoint.Charges,
                    chargeId,
                    OrderEndpoint.Cancel))
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
            AssertChargeReadDto(result);
        }
    }
}
