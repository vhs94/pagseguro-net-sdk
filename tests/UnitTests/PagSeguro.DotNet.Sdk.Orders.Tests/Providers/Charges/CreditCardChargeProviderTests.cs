using AutoFixture;
using FluentAssertions;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;
using PagSeguro.DotNet.Sdk.Orders.Providers.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Charges
{
    public class CreditCardChargeProviderTests
        : BaseCreditCardChargeProviderTests<
            ICreditCardChargeProvider,
            ChargeByCreditCardWriteDto,
            ChargeByCreditCardReadDto>
    {
        protected override ICreditCardChargeProvider CreateProvider()
        {
            return new CreditCardChargeProvider(Settings);
        }

        [Fact]
        public void AddPaymentMethod_PaymentMethodIsValid_PaymentMethodIsSet()
        {
            CreditCardPaymentMethodWriteDto paymentMethodWriteDto = CreateCreditCardPaymentMethodWriteDto();

            TopLevelProviderMock.AddPaymentMethod(paymentMethodWriteDto);

            TopLevelProviderMock
                .Build()
                .PaymentMethod
                .Should().BeEquivalentTo(paymentMethodWriteDto);
        }

        private CreditCardPaymentMethodWriteDto CreateCreditCardPaymentMethodWriteDto()
        {
            return Fixture.Create<CreditCardPaymentMethodWriteDto>();
        }

        [Fact]
        public async Task CreditCardChargeProvider_AssertAvailableMethods()
        {
            var provider = Substitute.For<ICreditCardChargeProvider>();

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
