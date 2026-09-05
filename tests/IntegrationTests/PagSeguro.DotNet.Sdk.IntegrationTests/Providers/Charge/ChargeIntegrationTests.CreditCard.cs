using AutoFixture;
using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers.Charge
{
    public partial class ChargeIntegrationTests
    {
        [Fact]
        public async Task CreateAsync_WithCreditCard_ChargeIsCreated()
        {
            CreditCardPaymentMethodRequest paymentMethodRequest = CreateCreditCardPaymentMethodRequest();
            ChargeByCreditCardRequest chargeRequest = CreateChargeByCreditCardRequest(paymentMethodRequest);

            ChargeByCreditCardResponse result = await Client
               .ForCharge()
               .WithCreditCard()
               .Load(chargeRequest)
               .ChargeAsync();

            await Task.Delay(1000);
            ChargeByCreditCardResponse chargeByCreditCardResponse = await Client
                .ForCharge()
                .WithCreditCard()
                .GetByIdAsync(result.Id!);
            AssertChargeWithAutoCapture(result, chargeRequest);
            AssertCreditCardPaymentMethodResponse(result.PaymentMethod!, paymentMethodRequest);
            result.Should().BeEquivalentTo(chargeByCreditCardResponse);
        }

        private CreditCardPaymentMethodRequest CreateCreditCardPaymentMethodRequest(
            bool capture = true)
        {
            return Fixture.Build<CreditCardPaymentMethodRequest>()
                .With(pm => pm.Installments, 1)
                .With(pm => pm.Capture, capture)
                .With(pm => pm.SoftDescriptor, "MyStore")
                .With(pm => pm.Card, CreateCardRequest())
                .Create();
        }

        private CardRequest CreateCardRequest()
        {
            return Fixture.Build<CardRequest>()
                .With(cc => cc.Number, "4111111111111111")
                .With(cc => cc.ExpMonth, 3)
                .With(cc => cc.ExpYear, 2023)
                .With(cc => cc.SecurityCode, 123)
                .Create();
        }

        private ChargeByCreditCardRequest CreateChargeByCreditCardRequest(
            CreditCardPaymentMethodRequest paymentMethodRequest)
        {
            return Client
                .ForCharge()
                .WithCreditCard()
                .AddPaymentMethod(paymentMethodRequest)
                .WithMetadata(CreateMetadata())
                .WithAmount(CreateAmountRequest())
                .WithReferenceId("ex-00001")
                .WithDescription("Motivo do pagamento")
                .WithNotificationUrl("https://myurl.com")
                .Build();
        }

        private IDictionary<string, string> CreateMetadata()
        {
            return Fixture.Create<IDictionary<string, string>>();
        }

        private static ChargeAmountRequest CreateAmountRequest()
        {
            return new ChargeAmountRequest
            {
                Currency = "BRL",
                Value = 1000
            };
        }

        private void AssertChargeWithAutoCapture(
            ChargeByCardResponse receivedChargeResponse,
            ChargeByCardRequest expectedChargeRequest)
        {
            AssertChargeResponse(receivedChargeResponse, expectedChargeRequest);
            receivedChargeResponse.PaymentResponse!.Reference.Should().Be("032416400102");
            receivedChargeResponse.Amount!.Summary!.Paid.Should().Be(1000);
        }

        private void AssertChargeResponse(
            ChargeByCardResponse receivedChargeResponse,
            ChargeByCardRequest expectedChargeRequest)
        {
            receivedChargeResponse.Should().NotBeNull();
            receivedChargeResponse.Should().BeEquivalentTo(expectedChargeRequest, options => options.ExcludingMissingMembers());
            receivedChargeResponse.CreatedDate.Date.Should().Be(DateTime.UtcNow.Date);
            receivedChargeResponse.Status.Should().Be("PAID");
            receivedChargeResponse.PaidDate.Should().NotBeNull();
            receivedChargeResponse.PaidDate!.Value.Date.Should().Be(DateTime.UtcNow.Date);
            receivedChargeResponse.PaymentResponse!.Message!.Should().Be("SUCESSO");
            receivedChargeResponse.PaymentResponse.Code.Should().Be(20000);
            receivedChargeResponse.Amount!.Summary!.Total.Should().Be(1000);
            receivedChargeResponse.Amount.Summary.Refunded.Should().Be(0);
            receivedChargeResponse.Links.Should().NotBeNullOrEmpty();
        }

        private void AssertCreditCardPaymentMethodResponse(
            CreditCardPaymentMethodResponse receivedPaymentMethod,
            CreditCardPaymentMethodRequest expectedPaymentMethod)
        {
            receivedPaymentMethod.Should().BeEquivalentTo(
                expectedPaymentMethod,
                options => options.ExcludingMissingMembers());
            AssertCartResponse(receivedPaymentMethod.Card);
        }

        private static void AssertCartResponse(CardResponse? cardResponse)
        {
            cardResponse.Should().NotBeNull();
            cardResponse.Brand.Should().Be("visa");
            cardResponse.FirstDigits.Should().Be(411111);
            cardResponse.LastDigits.Should().Be(1111);
        }

        [Fact]
        public async Task CaptureAsync_WithCreditCard_ChargeIsCaptured()
        {
            CreditCardPaymentMethodRequest paymentMethodRequest = CreateCreditCardPaymentMethodRequest(false);
            ChargeByCreditCardRequest chargeRequest = Client
                .ForCharge()
                .WithCreditCard()
                .AddPaymentMethod(paymentMethodRequest)
                .WithMetadata(CreateMetadata())
                .WithAmount(CreateAmountRequest())
                .WithReferenceId("ex-00001")
                .WithDescription("Motivo do pagamento")
                .WithNotificationUrl("https://myurl.com")
                .Build();
            ChargeByCreditCardResponse chargeResponse = await Client
               .ForCharge()
               .WithCreditCard()
               .Load(chargeRequest)
               .ChargeAsync();
            await Task.Delay(1000);

            ChargeByCreditCardResponse result = await Client
               .ForCharge()
               .WithCreditCard()
               .WithId(chargeResponse.Id!)
               .CaptureAsync(100);

            AssertChargeWithPreAuthorizedCapture(result, chargeRequest);
            AssertCreditCardPaymentMethodResponse(result.PaymentMethod!, paymentMethodRequest);
        }

        private void AssertChargeWithPreAuthorizedCapture(
            ChargeByCardResponse receivedChargeResponse,
            ChargeByCardRequest expectedChargeRequest)
        {
            AssertChargeResponse(receivedChargeResponse, expectedChargeRequest);
            receivedChargeResponse.PaymentResponse!.Reference!.Should().Be("31022400001");
            receivedChargeResponse.Amount!.Summary!.Paid.Should().Be(100);
        }
    }
}
