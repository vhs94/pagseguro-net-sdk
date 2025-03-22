using AutoFixture;
using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByBankSlip;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers.Charge
{
    public partial class ChargeIntegrationTests : BaseIntegrationTests
    {
        [Fact(Skip = "Payslip integration is broken. Waiting Pagseguro support")]
        public async Task CreateAsync_WithBankSlip_ChargeIsCreated()
        {
            BankSlipWriteDto bankSlipWriteDto = CreateBankSlip();
            ChargeByBankSlipWriteDto chargeWriteDto = CreateChargeByBankSlipWriteDto(bankSlipWriteDto);

            ChargeByBankSlipReadDto result = await Client
               .ForCharge()
               .WithBankSlip()
               .Load(chargeWriteDto)
               .ChargeAsync();

            await Task.Delay(1000);
            ChargeByBankSlipReadDto chargeByBankSlipReadDto = await Client
                .ForCharge()
                .WithBankSlip()
                .GetByIdAsync(result.Id!);
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(
                chargeWriteDto,
                options => options.Excluding(c => c.PaymentMethod!.BankSlip!.DueDate));
            result.PaymentMethod!.BankSlip!.DueDate.Should().Be(bankSlipWriteDto.DueDate.Date);
            result.Id.Should().StartWith("CHAR");
            result.CreatedDate.Date.Should().Be(DateTime.UtcNow.Date);
            result.Links.Should().NotBeNullOrEmpty();
            result.Amount!.Summary!.Paid.Should().Be(0);
            result.Amount.Summary.Total.Should().Be(1000);
            result.Amount.Summary.Refunded.Should().Be(0);
            result.PaymentResponse!.Message.Should().Be("SUCESSO");
            result.PaymentResponse.Code.Should().Be(20000);
            result.Should().BeEquivalentTo(chargeByBankSlipReadDto);
        }

        private ChargeByBankSlipWriteDto CreateChargeByBankSlipWriteDto(BankSlipWriteDto bankSlipWriteDto)
        {
            return Client
                .ForCharge()
                .WithBankSlip()
                .AddBankSlip(bankSlipWriteDto)
                .WithAmount(new ChargeAmountWriteDto
                {
                    Value = 1000,
                    Currency = "BRL"
                })
                .WithReferenceId("ex-00001")
                .WithDescription("Motivo do pagamento")
                .WithNotificationUrl("https://myurl.com")
                .Build();
        }

        private BankSlipWriteDto CreateBankSlip()
        {
            var holderAddress = Fixture.Build<AddressDto>()
                .With(h => h.Number, "1384")
                .With(h => h.Locality, "Pinheiros")
                .With(h => h.City, "Sao Paulo")
                .With(h => h.Region, "Sao Paulo")
                .With(h => h.RegionCode, "SP")
                .With(h => h.Country, "Brasil")
                .With(h => h.PostalCode, "01452002")
                .Create();
            var holder = Fixture.Build<BankSlipHolderDto>()
                .With(h => h.Address, holderAddress)
                .With(h => h.Email, "email@teste.com")
                .With(h => h.TaxId, "22222222222")
                .Create();
            return Fixture.Build<BankSlipWriteDto>()
                .With(b => b.DueDate, DateTime.Now.AddYears(1))
                .With(b => b.Holder, holder)
                .Create();
        }
    }
}
