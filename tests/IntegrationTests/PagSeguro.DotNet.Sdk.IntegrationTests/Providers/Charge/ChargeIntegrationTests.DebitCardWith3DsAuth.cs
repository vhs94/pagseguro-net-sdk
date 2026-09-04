using AutoFixture;
using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers.Charge
{
    public partial class ChargeIntegrationTests
    {
        [Fact]
        public async Task CreateAsync_WithDebitCardAnd3DsAuthentication_ChargeIsCreated()
        {
            AuthenticationMethodRequest authenticationMethodRequest = CreateAuthenticationMethodRequest();
            DebitCardWith3DsAuthPaymentMethodRequest paymentMethodRequest =
                CreateDebitCardWith3DsAuthPaymentMethodRequest(authenticationMethodRequest);
            ChargeByDebitCardWith3DsAuthRequest chargeRequest = Client
                .ForCharge()
                .WithDebitCardAnd3DsAuthentication()
                .AddPaymentMethod(paymentMethodRequest)
                .WithMetadata(CreateMetadata())
                .WithAmount(CreateAmountRequest())
                .WithReferenceId("ex-00001")
                .WithDescription("Motivo do pagamento")
                .WithNotificationUrl("https://myurl.com")
                .Build();

            ChargeByDebitCardWith3DsAuthResponse result = await Client
               .ForCharge()
               .WithDebitCardAnd3DsAuthentication()
               .Load(chargeRequest)
               .ChargeAsync();

            await Task.Delay(1000);
            ChargeByDebitCardWith3DsAuthResponse chargeByDebitCardWith3DsAuthResponse = await Client
                .ForCharge()
                .WithDebitCardAnd3DsAuthentication()
                .GetByIdAsync(result.Id!);
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(chargeRequest, options => options.ExcludingMissingMembers());
            result.CreatedDate.Date.Should().Be(DateTime.UtcNow.Date);
            result.Amount!.Summary!.Total.Should().Be(1000);
            result.Amount.Summary.Refunded.Should().Be(0);
            result.Links.Should().NotBeNullOrEmpty();

            // TODO: Debit card is not enabled in the PagBank sandbox - 3DS always comes back
            // NOT_AUTHENTICATED and the charge is DECLINED with 20017 (TRANSACAO NAO PERMITIDA),
            // even with PagBank's documented approval test card. The same authentication_method
            // payload authenticates fine for credit card, so this looks like a sandbox-side
            // limitation rather than an SDK bug. Re-enable these assertions once Pagseguro
            // support confirms debit + 3DS works in sandbox. Also double-check the hardcoded
            // Reference "032416400102" below when re-enabling: it was copied from the
            // credit-card flow (AssertChargeWithAutoCapture) and may not apply to debit.
            // result.Status.Should().Be("PAID");
            // result.PaidDate.Should().NotBeNull();
            // result.PaymentResponse!.Message!.Should().Be("SUCESSO");
            // result.PaymentResponse.Code.Should().Be(20000);
            // result.PaymentResponse.Reference.Should().Be("032416400102");
            // result.Amount.Summary.Paid.Should().Be(1000);

            AssertDebitCardPaymentMethodResponse(result.PaymentMethod!, paymentMethodRequest);

            result.PaymentMethod!.AuthenticationMethod.Should().BeEquivalentTo(
                authenticationMethodRequest,
                options => options.ExcludingMissingMembers());
            // TODO: sandbox always declines debit + 3DS authentication (see note above).
            // result.PaymentMethod.AuthenticationMethod!.Status.Should().Be("AUTHENTICATED");

            result.Should().BeEquivalentTo(
                chargeByDebitCardWith3DsAuthResponse,
                options => options
                    .Excluding(f => f.PaymentMethod!.AuthenticationMethod!));
        }

        private DebitCardWith3DsAuthPaymentMethodRequest CreateDebitCardWith3DsAuthPaymentMethodRequest(
            AuthenticationMethodRequest authenticationMethodRequest)
        {
            return Fixture.Build<DebitCardWith3DsAuthPaymentMethodRequest>()
                .With(pm => pm.Card, CreateCardRequest())
                .With(pm => pm.AuthenticationMethod, authenticationMethodRequest)
                .Create();
        }

        private void AssertDebitCardPaymentMethodResponse(
            DebitCardWith3DsAuthPaymentMethodResponse receivedPaymentMethodResponse,
            DebitCardWith3DsAuthPaymentMethodRequest expectedPaymentMethodRequest)
        {
            receivedPaymentMethodResponse.Should().BeEquivalentTo(
                expectedPaymentMethodRequest,
                options => options.ExcludingMissingMembers());
            AssertCartResponse(receivedPaymentMethodResponse.Card);
        }
    }
}
