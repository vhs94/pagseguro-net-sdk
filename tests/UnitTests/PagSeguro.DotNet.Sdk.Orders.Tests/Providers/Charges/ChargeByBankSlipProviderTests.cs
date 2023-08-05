using AutoFixture;
using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByBankSlip;
using PagSeguro.DotNet.Sdk.Orders.Providers.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Charges
{
    public class ChargeByBankSlipProviderTests : GenericChargeProviderTests<ChargeByBankSlipWriteDto, ChargeByBankSlipReadDto>
    {
        private ChargeByBankSlipProvider _chargeByBankSlipProviderMock;

        protected override GenericChargeProvider<ChargeByBankSlipWriteDto, ChargeByBankSlipReadDto> CreateProvider()
        {
            _chargeByBankSlipProviderMock = new ChargeByBankSlipProvider(Settings);
            return _chargeByBankSlipProviderMock;
        }

        protected override void AssertChargeResponse(
            ChargeByBankSlipReadDto expectedReadDto,
            ChargeByBankSlipReadDto receivedReadDto)
        {
            receivedReadDto
                .Should()
                .BeEquivalentTo(
                    expectedReadDto,
                    options => options.Excluding(f => f.PaymentMethod.BankSlip.DueDate));
            receivedReadDto
                .PaymentMethod.BankSlip.DueDate
                .Should()
                .Be(expectedReadDto.PaymentMethod.BankSlip.DueDate.Date);
        }

        [Fact]
        public void AddBankSlip_BankSlipIsValid_BankSlipIsSet()
        {
            BankSlipWriteDto bankSlipDto = CreateBankSlipWriteDto();

            _chargeByBankSlipProviderMock.AddBankSlip(bankSlipDto);

            _chargeByBankSlipProviderMock
                .Build()
                .PaymentMethod
                .BankSlip
                .Should().BeEquivalentTo(bankSlipDto);
        }

        private BankSlipWriteDto CreateBankSlipWriteDto()
        {
            return Fixture.Create<BankSlipWriteDto>();
        }

        [Fact]
        public void WithAmount_AmountIsValid_AmountIsSet()
        {
            ChargeAmountWriteDto chargeAmountWriteDto = CreateChargeAmountWriteDto();

            _chargeByBankSlipProviderMock.WithAmount(chargeAmountWriteDto);

            _chargeByBankSlipProviderMock
                .Build()
                .Amount
                .Should().BeEquivalentTo(chargeAmountWriteDto);
        }
    }
}
