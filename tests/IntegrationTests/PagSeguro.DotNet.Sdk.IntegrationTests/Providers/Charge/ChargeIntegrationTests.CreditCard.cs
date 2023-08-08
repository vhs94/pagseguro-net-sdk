using AutoFixture;
using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers.Charge
{
    public partial class ChargeIntegrationTests
    {
        [Fact]
        public async Task CreateAsync_WithCreditCard_ChargeIsCreated()
        {
            CreditCardPaymentMethodWriteDto paymentMethodDto = CreateCreditCardPaymentMethodWriteDto();
            ChargeByCreditCardWriteDto chargeWriteDto = CreateChargeByCreditCardWriteDto(paymentMethodDto);

            ChargeByCreditCardReadDto result = await Client
               .ForCharge()
               .WithCreditCard()
               .Load(chargeWriteDto)
               .ChargeAsync();

            await Task.Delay(1000);
            ChargeByCreditCardReadDto chargeByCreditCardReadDto = await Client
                .ForCharge()
                .WithCreditCard()
                .GetByIdAsync(result.Id);
            AssertChargeWithAutoCapture(result, chargeWriteDto);
            AssertCreditCardPaymentMethodReadDto(result.PaymentMethod, paymentMethodDto);
            result.Should().BeEquivalentTo(chargeByCreditCardReadDto);
        }

        private CreditCardPaymentMethodWriteDto CreateCreditCardPaymentMethodWriteDto(
            bool capture = true)
        {
            return Fixture.Build<CreditCardPaymentMethodWriteDto>()
                .With(pm => pm.Installments, 1)
                .With(pm => pm.Capture, capture)
                .With(pm => pm.SoftDescriptor, "MyStore")
                .With(pm => pm.Card, CreateCardWriteDto())
                .Create();
        }

        private CardWriteDto CreateCardWriteDto()
        {
            return Fixture.Build<CardWriteDto>()
                .With(cc => cc.Number, "4111111111111111")
                .With(cc => cc.ExpMonth, 3)
                .With(cc => cc.ExpYear, 2023)
                .With(cc => cc.SecurityCode, 123)
                .Create();
        }

        private ChargeByCreditCardWriteDto CreateChargeByCreditCardWriteDto(
            CreditCardPaymentMethodWriteDto paymentMethodDto)
        {
            return Client
                .ForCharge()
                .WithCreditCard()
                .AddPaymentMethod(paymentMethodDto)
                .WithMetadata(CreateMetadata())
                .WithAmount(CreateAmountWriteDto())
                .WithReferenceId("ex-00001")
                .WithDescription("Motivo do pagamento")
                .WithNotificationUrl("https://myurl.com")
                .Build();
        }

        private IDictionary<string, string> CreateMetadata()
        {
            return Fixture.Create<IDictionary<string, string>>();
        }

        private ChargeAmountWriteDto CreateAmountWriteDto()
        {
            return new ChargeAmountWriteDto
            {
                Currency = "BRL",
                Value = 1000
            };
        }

        private void AssertChargeWithAutoCapture(
            ChargeByCardReadDto receivedChargeDto,
            ChargeByCardWriteDto expectedChargeDto)
        {
            AssertChargeReadDto(receivedChargeDto, expectedChargeDto);
            receivedChargeDto.PaymentResponse.Reference.Should().Be("032416400102");
            receivedChargeDto.Amount.Summary.Paid.Should().Be(1000);
        }

        private void AssertChargeReadDto(
            ChargeByCardReadDto receivedChargeDto,
            ChargeByCardWriteDto expectedChargeDto)
        {
            receivedChargeDto.Should().NotBeNull();
            receivedChargeDto.Should().BeEquivalentTo(expectedChargeDto, options => options.ExcludingMissingMembers());
            receivedChargeDto.CreatedDate.Date.Should().Be(DateTime.UtcNow.Date);
            receivedChargeDto.PaidDate.Value.Date.Should().Be(DateTime.UtcNow.Date);
            receivedChargeDto.Status.Should().Be("PAID");
            receivedChargeDto.PaymentResponse.Message.Should().Be("SUCESSO");
            receivedChargeDto.PaymentResponse.Code.Should().Be(20000);
            receivedChargeDto.Amount.Summary.Total.Should().Be(1000);
            receivedChargeDto.Amount.Summary.Refunded.Should().Be(0);
            receivedChargeDto.Links.Should().NotBeNullOrEmpty();
        }

        private void AssertCreditCardPaymentMethodReadDto(
            CreditCardPaymentMethodReadDto receivedPaymentMethod,
            CreditCardPaymentMethodWriteDto expectedPaymentMethod)
        {
            receivedPaymentMethod.Should().BeEquivalentTo(
                expectedPaymentMethod,
                options => options.ExcludingMissingMembers());
            AssertCartReadDto(receivedPaymentMethod.Card);
        }

        private void AssertCartReadDto(CardReadDto cardReadDto)
        {
            cardReadDto.Brand.Should().Be("visa");
            cardReadDto.FirstDigits.Should().Be(411111);
            cardReadDto.LastDigits.Should().Be(1111);
        }

        [Fact]
        public async Task CaptureAsync_WithCreditCard_ChargeIsCaptured()
        {
            CreditCardPaymentMethodWriteDto paymentMethodDto = CreateCreditCardPaymentMethodWriteDto(false);
            ChargeByCreditCardWriteDto chargeWriteDto = Client
                .ForCharge()
                .WithCreditCard()
                .AddPaymentMethod(paymentMethodDto)
                .WithMetadata(CreateMetadata())
                .WithAmount(CreateAmountWriteDto())
                .WithReferenceId("ex-00001")
                .WithDescription("Motivo do pagamento")
                .WithNotificationUrl("https://myurl.com")
                .Build();
            ChargeByCreditCardReadDto chargeReadtDto = await Client
               .ForCharge()
               .WithCreditCard()
               .Load(chargeWriteDto)
               .ChargeAsync();
            await Task.Delay(1000);

            ChargeByCreditCardReadDto result = await Client
               .ForCharge()
               .WithCreditCard()
               .WithId(chargeReadtDto.Id)
               .CaptureAsync(100);

            AssertChargeWithPreAuthorizedCapture(result, chargeWriteDto);
            AssertCreditCardPaymentMethodReadDto(result.PaymentMethod, paymentMethodDto);
        }

        private void AssertChargeWithPreAuthorizedCapture(
            ChargeByCardReadDto receivedChargeDto,
            ChargeByCardWriteDto expectedChargeDto)
        {
            AssertChargeReadDto(receivedChargeDto, expectedChargeDto);
            receivedChargeDto.PaymentResponse.Reference.Should().Be("31022400001");
            receivedChargeDto.Amount.Summary.Paid.Should().Be(100);
        }
    }
}
