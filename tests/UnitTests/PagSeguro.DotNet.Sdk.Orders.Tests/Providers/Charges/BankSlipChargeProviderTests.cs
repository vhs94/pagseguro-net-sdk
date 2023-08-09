using AutoFixture;
using FluentAssertions;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByBankSlip;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;
using PagSeguro.DotNet.Sdk.Orders.Providers.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Charges
{
    public class BankSlipChargeProviderTests
        : BaseChargeProviderTests<
            IBankSlipChargeProvider,
            ChargeByBankSlipWriteDto,
            ChargeByBankSlipReadDto>
    {
        protected override IBankSlipChargeProvider CreateProvider()
        {
            return new BankSlipChargeProvider(Settings);
        }

        [Fact]
        public void AddBankSlip_BankSlipIsValid_BankSlipIsSet()
        {
            BankSlipWriteDto bankSlipDto = CreateBankSlipWriteDto();

            TopLevelProviderMock.AddBankSlip(bankSlipDto);

            TopLevelProviderMock.Build().PaymentMethod.BankSlip.Should().BeEquivalentTo(bankSlipDto);
        }

        private BankSlipWriteDto CreateBankSlipWriteDto()
        {
            return Fixture.Create<BankSlipWriteDto>();
        }

        protected override void AssertChargeReadDto(ChargeByBankSlipReadDto receivedChargeReadDto)
        {
            receivedChargeReadDto
                .Should()
                .BeEquivalentTo(
                    ChargeReadDto,
                    options => options.Excluding(f => f.PaymentMethod.BankSlip.DueDate));
            receivedChargeReadDto
                .PaymentMethod.BankSlip.DueDate
                .Should()
                .Be(ChargeReadDto.PaymentMethod.BankSlip.DueDate.Date);
        }

        [Fact]
        public async Task BankSlipChargeProvider_AssertAvailableMethods()
        {
            var provider = Substitute.For<IBankSlipChargeProvider>();

            provider
                .AddBankSlip(null)
                .WithReferenceId(null)
                .WithId(null)
                .WithAmount(null)
                .WithDescription(null)
                .WithNotificationUrl(null)
                .WithNotificationUrls(null)
                .Load(null)
                .Build();
            await provider.CancelAsync(0);
            await provider.ChargeAsync();
            await provider.GetByIdAsync(null);

            provider
                .Received(1)
                .AddBankSlip(null)
                .WithReferenceId(null)
                .WithId(null)
                .WithAmount(null)
                .WithDescription(null)
                .WithNotificationUrl(null)
                .WithNotificationUrls(null)
                .Load(null)
                .Build();
            await provider.Received(1).CancelAsync(0);
            await provider.Received(1).ChargeAsync();
            await provider.Received(1).GetByIdAsync(null);
        }
    }
}
