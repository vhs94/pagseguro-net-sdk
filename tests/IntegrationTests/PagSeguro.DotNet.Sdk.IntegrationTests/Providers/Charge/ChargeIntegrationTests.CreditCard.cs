using AutoFixture;
using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card;
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

            ChargeByCreditCardReadDto result = await Client
               .ForCharge()
               .WithCreditCard()
               .Load(chargeWriteDto)
               .ChargeAsync();

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(chargeWriteDto, options => options.ExcludingMissingMembers());
            result.CreatedDate.Date.Should().Be(DateTime.UtcNow.Date);
            result.PaidDate.Value.Date.Should().Be(DateTime.UtcNow.Date);
            result.Status.Should().Be("PAID");
            result.PaymentResponse.Message.Should().Be("SUCESSO");
            result.PaymentResponse.Code.Should().Be(20000);
            result.PaymentResponse.Reference.Should().Be("032416400102");
            result.PaymentMethod.Card.Brand.Should().Be("visa");
            result.PaymentMethod.Card.FirstDigits.Should().Be(411111);
            result.PaymentMethod.Card.LastDigits.Should().Be(1111);
            result.Links.Should().NotBeNullOrEmpty();
            result.Amount.Summary.Paid.Should().Be(1000);
            result.Amount.Summary.Total.Should().Be(1000);
            result.Amount.Summary.Refunded.Should().Be(0);
        }

        private CreditCardPaymentMethodWriteDto CreateCreditCardPaymentMethodWriteDto()
        {
            return Fixture.Build<CreditCardPaymentMethodWriteDto>()
                .With(pm => pm.Installments, 1)
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
    }
}
