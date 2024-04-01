using AutoFixture;
using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.AuthenticationMethod;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.DebitCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.DebitCard;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers.Charge
{
    public partial class ChargeIntegrationTests
    {
        [Fact]
        public async Task CreateAsync_WithDebitCardAnd3DsAuthentication_ChargeIsCreated()
        {
            AuthenticationMethodWriteDto authenticationMethodWriteDto = CreateAuthenticationMethodWriteDto();
            DebitCardWith3DsAuthPaymentMethodWriteDto paymentMethodDto =
                CreateDebitCardWith3DsAuthPaymentMethodWriteDto(authenticationMethodWriteDto);
            ChargeByDebitCardWith3DsAuthWriteDto chargeWriteDto = Client
                .ForCharge()
                .WithDebitCardAnd3DsAuthentication()
                .AddPaymentMethod(paymentMethodDto)
                .WithMetadata(CreateMetadata())
                .WithAmount(CreateAmountWriteDto())
                .WithReferenceId("ex-00001")
                .WithDescription("Motivo do pagamento")
                .WithNotificationUrl("https://myurl.com")
                .Build();

            ChargeByDebitCardWith3DsAuthReadDto result = await Client
               .ForCharge()
               .WithDebitCardAnd3DsAuthentication()
               .Load(chargeWriteDto)
               .ChargeAsync();

            await Task.Delay(1000);
            ChargeByDebitCardWith3DsAuthReadDto chargeByDebitCardWith3DsAuthReadDto = await Client
                .ForCharge()
                .WithDebitCardAnd3DsAuthentication()
                .GetByIdAsync(result.Id);
            AssertChargeWithAutoCapture(result, chargeWriteDto);
            AssertDebitCardPaymentMethodReadDto(result.PaymentMethod, paymentMethodDto);
            AssertAuthenticationMethodReadDto(result.PaymentMethod.AuthenticationMethod, authenticationMethodWriteDto);
            result.Should().BeEquivalentTo(
                chargeByDebitCardWith3DsAuthReadDto,
                options => options
                    .Excluding(f => f.PaymentMethod.AuthenticationMethod));
        }

        private DebitCardWith3DsAuthPaymentMethodWriteDto CreateDebitCardWith3DsAuthPaymentMethodWriteDto(
            AuthenticationMethodWriteDto authenticationMethodWriteDto)
        {
            return Fixture.Build<DebitCardWith3DsAuthPaymentMethodWriteDto>()
                .With(pm => pm.Card, CreateCardWriteDto())
                .With(pm => pm.AuthenticationMethod, authenticationMethodWriteDto)
                .Create();
        }

        private void AssertDebitCardPaymentMethodReadDto(
            DebitCardWith3DsAuthPaymentMethodReadDto receivedPaymentMethodDto,
            DebitCardWith3DsAuthPaymentMethodWriteDto expectedPaymentMethodDtoDto)
        {
            receivedPaymentMethodDto.Should().BeEquivalentTo(
                expectedPaymentMethodDtoDto,
                options => options.ExcludingMissingMembers());
            AssertCartReadDto(receivedPaymentMethodDto.Card);
        }
    }
}
