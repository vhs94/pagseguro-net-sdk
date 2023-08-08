using AutoFixture;
using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.AuthenticationMethod;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard.Auth3Ds;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers.Charge
{
    public partial class ChargeIntegrationTests
    {
        [Fact]
        public async Task CreateAsync_WithCreditCardAnd3DsAuthentication_ChargeIsCreated()
        {
            AuthenticationMethodWriteDto authenticationMethodWriteDto = CreateAuthenticationMethodWriteDto();
            CreditCardWith3DsAuthPaymentMethodWriteDto paymentMethodDto =
                CreateCreditCardWith3DsAuthPaymentMethodWriteDto(authenticationMethodWriteDto);
            ChargeByCreditCardWith3DsAuthWriteDto chargeWriteDto = Client
                .ForCharge()
                .WithCreditCardAnd3DsAuthentication()
                .AddPaymentMethod(paymentMethodDto)
                .WithMetadata(CreateMetadata())
                .WithAmount(CreateAmountWriteDto())
                .WithReferenceId("ex-00001")
                .WithDescription("Motivo do pagamento")
                .WithNotificationUrl("https://myurl.com")
                .Build();

            ChargeByCreditCardWith3DsAuthReadDto result = await Client
               .ForCharge()
               .WithCreditCardAnd3DsAuthentication()
               .Load(chargeWriteDto)
               .ChargeAsync();

            await Task.Delay(1000);
            ChargeByCreditCardWith3DsAuthReadDto chargeByCreditCardWith3DsAuthReadDto = await Client
                .ForCharge()
                .WithCreditCardAnd3DsAuthentication()
                .GetByIdAsync(result.Id);
            AssertChargeWithAutoCapture(result, chargeWriteDto);
            AssertCreditCardPaymentMethodReadDto(result.PaymentMethod, paymentMethodDto);
            AssertAuthenticationMethodReadDto(result.PaymentMethod.AuthenticationMethod, authenticationMethodWriteDto);
            result.Should().BeEquivalentTo(
                chargeByCreditCardWith3DsAuthReadDto,
                options => options
                    .Excluding(f => f.PaymentMethod.AuthenticationMethod));
        }

        private AuthenticationMethodWriteDto CreateAuthenticationMethodWriteDto()
        {
            return new AuthenticationMethodWriteDto
            {
                Type = "THREEDS",
                Cavv = "BwABBylVaQAAAAFwllVpAAAAAAA=",
                Xid = "BwABBylVaQAAAAFwllVpAAAAAAA=",
                Eci = "01",
                Version = "2.1.0",
                DstransId = "DIR_SERVER_TID"
            };
        }

        private CreditCardWith3DsAuthPaymentMethodWriteDto CreateCreditCardWith3DsAuthPaymentMethodWriteDto(
            AuthenticationMethodWriteDto authenticationMethodWriteDto,
            bool capture = true)
        {
            return Fixture.Build<CreditCardWith3DsAuthPaymentMethodWriteDto>()
                .With(pm => pm.Installments, 1)
                .With(pm => pm.SoftDescriptor, "MyStore")
                .With(pm => pm.Capture, capture)
                .With(pm => pm.Card, CreateCardWriteDto())
                .With(pm => pm.AuthenticationMethod, authenticationMethodWriteDto)
                .Create();
        }

        private void AssertAuthenticationMethodReadDto(
            AuthenticationMethodReadDto receivedAuthenticationMethodDto,
            AuthenticationMethodWriteDto expectedAuthenticationMethodWriteDto)
        {
            receivedAuthenticationMethodDto.Should().BeEquivalentTo(
                expectedAuthenticationMethodWriteDto,
                options => options.ExcludingMissingMembers());
            receivedAuthenticationMethodDto.Status.Should().Be("AUTHENTICATED");
        }

        [Fact]
        public async Task CaptureAsync_WithCreditCardAnd3DsAuthentication_ChargeIsCaptured()
        {
            AuthenticationMethodWriteDto authenticationMethodWriteDto = CreateAuthenticationMethodWriteDto();
            CreditCardWith3DsAuthPaymentMethodWriteDto paymentMethodDto =
                CreateCreditCardWith3DsAuthPaymentMethodWriteDto(authenticationMethodWriteDto, false);
            ChargeByCreditCardWith3DsAuthWriteDto chargeWriteDto = Client
                .ForCharge()
                .WithCreditCardAnd3DsAuthentication()
                .AddPaymentMethod(paymentMethodDto)
                .WithMetadata(CreateMetadata())
                .WithAmount(CreateAmountWriteDto())
                .WithReferenceId("ex-00001")
                .WithDescription("Motivo do pagamento")
                .WithNotificationUrl("https://myurl.com")
                .Build();
            ChargeByCreditCardWith3DsAuthReadDto chargeReadtDto = await Client
               .ForCharge()
               .WithCreditCardAnd3DsAuthentication()
               .Load(chargeWriteDto)
               .ChargeAsync();
            await Task.Delay(1000);

            ChargeByCreditCardWith3DsAuthReadDto result = await Client
               .ForCharge()
               .WithCreditCardAnd3DsAuthentication()
               .WithId(chargeReadtDto.Id)
               .CaptureAsync(100);

            AssertChargeWithPreAuthorizedCapture(result, chargeWriteDto);
            AssertCreditCardPaymentMethodReadDto(result.PaymentMethod, paymentMethodDto);
            result.PaymentMethod.AuthenticationMethod.Type.Should().Be(authenticationMethodWriteDto.Type);
            result.PaymentMethod.AuthenticationMethod.Eci.Should().Be(authenticationMethodWriteDto.Eci);
            result.PaymentMethod.AuthenticationMethod.Cavv.Should().Be(authenticationMethodWriteDto.Cavv);
            result.PaymentMethod.AuthenticationMethod.Status.Should().Be("AUTHENTICATED");
            result.PaymentMethod.AuthenticationMethod.Xid.Should().BeNull();
            result.PaymentMethod.AuthenticationMethod.Version.Should().BeNull();
            result.PaymentMethod.AuthenticationMethod.DstransId.Should().BeNull();
        }
    }
}
