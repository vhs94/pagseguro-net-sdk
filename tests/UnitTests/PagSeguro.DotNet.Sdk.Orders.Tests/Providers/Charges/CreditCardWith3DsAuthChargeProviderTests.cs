using AutoFixture;
using FluentAssertions;
using Flurl;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Providers.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Charges
{
    public class CreditCardWith3DsAuthChargeProviderTests : BaseProviderTests<CreditCardWith3DsAuthChargeProvider>
    {
        public ChargeByCreditCardWith3DsAuthResponse ChargeResponse { get; private set; } = null!;

        protected override CreditCardWith3DsAuthChargeProvider CreateProvider()
        {
            return new CreditCardWith3DsAuthChargeProvider(Settings, FlurlClientMock);
        }

        protected override void SetupMocks()
        {
            ChargeResponse = CreateChargeResponse();
            HttpTestMock
                .ForCallsTo(
                    Url.Combine(ProviderBaseUrl, OrderEndpoint.Charges),
                    Url.Combine(ProviderBaseUrl, OrderEndpoint.Charges, "*"))
                .WithVerb(HttpMethod.Post, HttpMethod.Get)
                .RespondWithJson(ChargeResponse);
        }

        private ChargeByCreditCardWith3DsAuthResponse CreateChargeResponse()
        {
            return Fixture.Create<ChargeByCreditCardWith3DsAuthResponse>();
        }

        [Fact]
        public void AddPaymentMethod_PaymentMethodIsValid_PaymentMethodIsSet()
        {
            CreditCardWith3DsAuthPaymentMethodRequest paymentMethodRequest =
                CreateCreditCardWith3DsAuthPaymentMethodRequest();

            Provider.AddPaymentMethod(paymentMethodRequest);

            Provider
                .Build()
                .PaymentMethod
                .Should().BeEquivalentTo(paymentMethodRequest);
        }

        private CreditCardWith3DsAuthPaymentMethodRequest CreateCreditCardWith3DsAuthPaymentMethodRequest()
        {
            return Fixture.Create<CreditCardWith3DsAuthPaymentMethodRequest>();
        }

        [Fact]
        public void WithMetadata_MetadataIsValid_MetadataIsSet()
        {
            IDictionary<string, string> metadata = CreateMetadata();

            Provider.WithMetadata(metadata);

            Provider.Build().Metadata.Should().BeEquivalentTo(metadata);
        }

        private IDictionary<string, string> CreateMetadata()
        {
            return Fixture.Create<IDictionary<string, string>>();
        }

        [Fact]
        public void WithAmount_AmountIsValid_AmountIsSet()
        {
            ChargeAmountRequest chargeAmountRequest = CreateChargeAmountRequest();

            Provider.WithAmount(chargeAmountRequest);

            Provider
                .Build()
                .Amount
                .Should().BeEquivalentTo(chargeAmountRequest);
        }

        private ChargeAmountRequest CreateChargeAmountRequest()
        {
            return Fixture.Create<ChargeAmountRequest>();
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
            var expectedCharge = new ChargeByCreditCardWith3DsAuthRequest
            {
                ReferenceId = referenceId
            };

            Provider.Load(expectedCharge);

            Provider
                .ChargeRequest
                .Should()
                .BeEquivalentTo(expectedCharge);
        }

        [Fact]
        public void Build_ChargeIsReturned()
        {
            string referenceId = "referenceId";
            var expectedCharge = new ChargeByCreditCardWith3DsAuthRequest
            {
                ReferenceId = referenceId
            };

            Provider.WithReferenceId(referenceId);
            ChargeByCreditCardWith3DsAuthRequest chargeRequest = Provider.Build();

            ChargeByCreditCardWith3DsAuthRequest secondChargeRequest = Provider.Build();
            chargeRequest.Should().BeEquivalentTo(expectedCharge);
            secondChargeRequest.Should().NotBeEquivalentTo(expectedCharge);
        }

        [Fact]
        public async Task ChargeAsync_ChargeIsValid_HttpRequestIsCreated()
        {
            ChargeByCreditCardWith3DsAuthResponse result = await Provider.ChargeAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(ProviderBaseUrl, OrderEndpoint.Charges))
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey)
                .WithRequestJson(Provider.Build())
                .WithVerb(HttpMethod.Post)
                .Times(1);
            result.Should().BeEquivalentTo(ChargeResponse);
        }

        [Fact]
        public async Task GetByIdAsync_ChargeIsValid_HttpRequestIsCreated()
        {
            string chargeId = Guid.NewGuid().ToString();

            ChargeByCreditCardWith3DsAuthResponse result = await Provider.GetByIdAsync(chargeId);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(
                    ProviderBaseUrl,
                    OrderEndpoint.Charges,
                    chargeId))
                .WithOAuthBearerToken(Settings.Token)
                .WithVerb(HttpMethod.Get)
                .Times(1);
            result.Should().BeEquivalentTo(ChargeResponse);
        }

        [Fact]
        public async Task CancelAsync_ChargeIsValid_HttpRequestIsCreated()
        {
            string chargeId = Guid.NewGuid().ToString();

            ChargeByCreditCardWith3DsAuthResponse result = await Provider.WithId(chargeId).CancelAsync(100);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(
                    ProviderBaseUrl,
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
            result.Should().BeEquivalentTo(ChargeResponse);
        }

        [Fact]
        public async Task CaptureAsync_ChargeIsValid_HttpRequestIsCreated()
        {
            string chargeId = Guid.NewGuid().ToString();

            ChargeByCreditCardWith3DsAuthResponse result = await Provider
                .WithId(chargeId)
                .CaptureAsync(100);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(
                    ProviderBaseUrl,
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
            result.Should().BeEquivalentTo(ChargeResponse);
        }

        [Fact]
        public async Task CreditCardWith3DsAuthChargeProvider_AssertAvailableMethods()
        {
            var provider = Substitute.For<ICreditCardWith3DsAuthChargeProvider>();

            provider
                .AddPaymentMethod(null!)
                .WithReferenceId(null!)
                .WithId(null!)
                .WithAmount(null!)
                .WithDescription(null!)
                .WithNotificationUrl(null!)
                .WithNotificationUrls(null!)
                .WithMetadata(null!)
                .Load(null!)
                .Build();
            await provider.CancelAsync(0);
            await provider.ChargeAsync();
            await provider.GetByIdAsync(null!);
            await provider.CaptureAsync(0);

            provider
                .Received(1)
                .AddPaymentMethod(null!)
                .WithReferenceId(null!)
                .WithId(null!)
                .WithAmount(null!)
                .WithDescription(null!)
                .WithNotificationUrl(null!)
                .WithNotificationUrls(null!)
                .WithMetadata(null!)
                .Load(null!)
                .Build();
            await provider.Received(1).CancelAsync(0);
            await provider.Received(1).ChargeAsync();
            await provider.Received(1).GetByIdAsync(null!);
            await provider.Received(1).CaptureAsync(0);
        }
    }
}
