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

            AssertChargeByCardReadDto(result, chargeWriteDto);
            AssertCreditCardPaymentMethodReadDto(result.PaymentMethod, paymentMethodDto);
            AssertAuthenticationMethodReadDto(result.PaymentMethod.AuthenticationMethod, authenticationMethodWriteDto);
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
            AuthenticationMethodWriteDto authenticationMethodWriteDto)
        {
            return Fixture.Build<CreditCardWith3DsAuthPaymentMethodWriteDto>()
                .With(pm => pm.Installments, 1)
                .With(pm => pm.SoftDescriptor, "MyStore")
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
    }
}
