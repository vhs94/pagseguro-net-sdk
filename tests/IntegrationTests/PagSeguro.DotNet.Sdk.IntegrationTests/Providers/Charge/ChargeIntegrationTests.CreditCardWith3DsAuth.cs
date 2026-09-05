using AutoFixture;
using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers.Charge
{
    public partial class ChargeIntegrationTests
    {
        [Fact]
        public async Task CreateAsync_WithCreditCardAnd3DsAuthentication_ChargeIsCreated()
        {
            AuthenticationMethodRequest authenticationMethodRequest = CreateAuthenticationMethodRequest();
            CreditCardWith3DsAuthPaymentMethodRequest paymentMethodRequest =
                CreateCreditCardWith3DsAuthPaymentMethodRequest(authenticationMethodRequest);
            ChargeByCreditCardWith3DsAuthRequest chargeRequest = Client
                .ForCharge()
                .WithCreditCardAnd3DsAuthentication()
                .AddPaymentMethod(paymentMethodRequest)
                .WithMetadata(CreateMetadata())
                .WithAmount(CreateAmountRequest())
                .WithReferenceId("ex-00001")
                .WithDescription("Motivo do pagamento")
                .WithNotificationUrl("https://myurl.com")
                .Build();

            ChargeByCreditCardWith3DsAuthResponse result = await Client
               .ForCharge()
               .WithCreditCardAnd3DsAuthentication()
               .Load(chargeRequest)
               .ChargeAsync();

            await Task.Delay(1000);
            ChargeByCreditCardWith3DsAuthResponse chargeByCreditCardWith3DsAuthResponse = await Client
                .ForCharge()
                .WithCreditCardAnd3DsAuthentication()
                .GetByIdAsync(result.Id!);
            AssertChargeWithAutoCapture(result, chargeRequest);
            AssertCreditCardPaymentMethodResponse(result.PaymentMethod!, paymentMethodRequest);
            AssertAuthenticationMethodResponse(result.PaymentMethod!.AuthenticationMethod!, authenticationMethodRequest);
            result.Should().BeEquivalentTo(
                chargeByCreditCardWith3DsAuthResponse,
                options => options
                    .Excluding(f => f.PaymentMethod!.AuthenticationMethod!));
        }

        private static AuthenticationMethodRequest CreateAuthenticationMethodRequest()
        {
            return new AuthenticationMethodRequest
            {
                Type = "THREEDS",
                Cavv = "BwABBylVaQAAAAFwllVpAAAAAAA=",
                Xid = "BwABBylVaQAAAAFwllVpAAAAAAA=",
                Eci = "01",
                Version = "2.1.0",
                DstransId = "DIR_SERVER_TID"
            };
        }

        private CreditCardWith3DsAuthPaymentMethodRequest CreateCreditCardWith3DsAuthPaymentMethodRequest(
            AuthenticationMethodRequest authenticationMethodRequest,
            bool capture = true)
        {
            return Fixture.Build<CreditCardWith3DsAuthPaymentMethodRequest>()
                .With(pm => pm.Installments, 1)
                .With(pm => pm.SoftDescriptor, "MyStore")
                .With(pm => pm.Capture, capture)
                .With(pm => pm.Card, CreateCardRequest())
                .With(pm => pm.AuthenticationMethod, authenticationMethodRequest)
                .Create();
        }

        private static void AssertAuthenticationMethodResponse(
            AuthenticationMethodResponse receivedAuthenticationMethodResponse,
            AuthenticationMethodRequest expectedAuthenticationMethodRequest)
        {
            receivedAuthenticationMethodResponse.Should().BeEquivalentTo(
                expectedAuthenticationMethodRequest,
                options => options.ExcludingMissingMembers());
            receivedAuthenticationMethodResponse.Status.Should().Be("AUTHENTICATED");
        }

        [Fact]
        public async Task CaptureAsync_WithCreditCardAnd3DsAuthentication_ChargeIsCaptured()
        {
            AuthenticationMethodRequest authenticationMethodRequest = CreateAuthenticationMethodRequest();
            CreditCardWith3DsAuthPaymentMethodRequest paymentMethodRequest =
                CreateCreditCardWith3DsAuthPaymentMethodRequest(authenticationMethodRequest, false);
            ChargeByCreditCardWith3DsAuthRequest chargeRequest = Client
                .ForCharge()
                .WithCreditCardAnd3DsAuthentication()
                .AddPaymentMethod(paymentMethodRequest)
                .WithMetadata(CreateMetadata())
                .WithAmount(CreateAmountRequest())
                .WithReferenceId("ex-00001")
                .WithDescription("Motivo do pagamento")
                .WithNotificationUrl("https://myurl.com")
                .Build();
            ChargeByCreditCardWith3DsAuthResponse chargeResponse = await Client
               .ForCharge()
               .WithCreditCardAnd3DsAuthentication()
               .Load(chargeRequest)
               .ChargeAsync();
            await Task.Delay(1000);

            ChargeByCreditCardWith3DsAuthResponse result = await Client
               .ForCharge()
               .WithCreditCardAnd3DsAuthentication()
               .WithId(chargeResponse.Id!)
               .CaptureAsync(100);

            AssertChargeWithPreAuthorizedCapture(result, chargeRequest);
            AssertCreditCardPaymentMethodResponse(result.PaymentMethod!, paymentMethodRequest);
            result.PaymentMethod!.AuthenticationMethod!.Type.Should().Be(authenticationMethodRequest.Type);
            result.PaymentMethod.AuthenticationMethod.Eci.Should().Be(authenticationMethodRequest.Eci);
            result.PaymentMethod.AuthenticationMethod.Cavv.Should().Be(authenticationMethodRequest.Cavv);
            result.PaymentMethod.AuthenticationMethod.Status.Should().Be("AUTHENTICATED");
            result.PaymentMethod.AuthenticationMethod.Xid.Should().BeNull();
            result.PaymentMethod.AuthenticationMethod.Version.Should().BeNull();
            result.PaymentMethod.AuthenticationMethod.DstransId.Should().BeNull();
        }
    }
}
