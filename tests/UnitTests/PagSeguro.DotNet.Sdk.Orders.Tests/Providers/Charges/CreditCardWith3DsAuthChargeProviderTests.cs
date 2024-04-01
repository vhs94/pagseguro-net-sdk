using AutoFixture;
using FluentAssertions;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard.Auth3Ds;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;
using PagSeguro.DotNet.Sdk.Orders.Providers.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Charges
{
    public class CreditCardWith3DsAuthChargeProviderTests
        : BaseCreditCardChargeProviderTests<
            ICreditCardWith3DsAuthChargeProvider,
            ChargeByCreditCardWith3DsAuthWriteDto,
            ChargeByCreditCardWith3DsAuthReadDto>
    {
        protected override ICreditCardWith3DsAuthChargeProvider CreateProvider()
        {
            return new CreditCardWith3DsAuthChargeProvider(Settings);
        }

        [Fact]
        public void AddPaymentMethod_PaymentMethodIsValid_PaymentMethodIsSet()
        {
            CreditCardWith3DsAuthPaymentMethodWriteDto paymentMethodWriteDto =
                CreateCreditCardWith3DsAuthPaymentMethodWriteDto();

            TopLevelProviderMock.AddPaymentMethod(paymentMethodWriteDto);

            TopLevelProviderMock
                .Build()
                .PaymentMethod
                .Should().BeEquivalentTo(paymentMethodWriteDto);
        }

        private CreditCardWith3DsAuthPaymentMethodWriteDto CreateCreditCardWith3DsAuthPaymentMethodWriteDto()
        {
            return Fixture.Create<CreditCardWith3DsAuthPaymentMethodWriteDto>();
        }

        [Fact]
        public async Task CreditCardWith3DsAuthChargeProvider_AssertAvailableMethods()
        {
            var provider = Substitute.For<ICreditCardWith3DsAuthChargeProvider>();

            provider
                .AddPaymentMethod(null)
                .WithReferenceId(null)
                .WithId(null)
                .WithAmount(null)
                .WithDescription(null)
                .WithNotificationUrl(null)
                .WithNotificationUrls(null)
                .WithMetadata(null)
                .Load(null)
                .Build();
            await provider.CancelAsync(0);
            await provider.ChargeAsync();
            await provider.GetByIdAsync(null);
            await provider.CaptureAsync(0);

            provider
                .Received(1)
                .AddPaymentMethod(null)
                .WithReferenceId(null)
                .WithId(null)
                .WithAmount(null)
                .WithDescription(null)
                .WithNotificationUrl(null)
                .WithNotificationUrls(null)
                .WithMetadata(null)
                .Load(null)
                .Build();
            await provider.Received(1).CancelAsync(0);
            await provider.Received(1).ChargeAsync();
            await provider.Received(1).GetByIdAsync(null);
            await provider.Received(1).CaptureAsync(0);
        }
    }
}
