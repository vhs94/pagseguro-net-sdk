using AutoFixture;
using FluentAssertions;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.DebitCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.DebitCard;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;
using PagSeguro.DotNet.Sdk.Orders.Providers.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Charges
{
    public class DebitCardWith3DsAuthChargeProviderTests
        : BaseCardChargeProviderTests<
            IDebitCardWith3DsAuthChargeProvider,
            ChargeByDebitCardWith3DsAuthWriteDto,
            ChargeByDebitCardWith3DsAuthReadDto>
    {
        protected override IDebitCardWith3DsAuthChargeProvider CreateProvider()
        {
            return new DebitCardWith3DsAuthChargeProvider(Settings);
        }

        [Fact]
        public void AddPaymentMethod_PaymentMethodIsValid_PaymentMethodIsSet()
        {
            DebitCardWith3DsAuthPaymentMethodWriteDto paymentMethodWriteDto =
                CreateDebitCardWith3DsAuthPaymentMethodWriteDto();

            TopLevelProviderMock.AddPaymentMethod(paymentMethodWriteDto);

            TopLevelProviderMock
                .Build()
                .PaymentMethod
                .Should().BeEquivalentTo(paymentMethodWriteDto);
        }

        private DebitCardWith3DsAuthPaymentMethodWriteDto CreateDebitCardWith3DsAuthPaymentMethodWriteDto()
        {
            return Fixture.Create<DebitCardWith3DsAuthPaymentMethodWriteDto>();
        }

        [Fact]
        public async Task DebitCardWith3DsAuthChargeProvider_AssertAvailableMethods()
        {
            var provider = Substitute.For<IDebitCardWith3DsAuthChargeProvider>();

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
        }
    }
}
