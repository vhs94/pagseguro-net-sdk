using AutoFixture;
using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers.Charge
{
    public partial class ChargeIntegrationTests : BaseIntegrationTests
    {
        [Fact]
        public async Task CreateAsync_WithBankSlip_ChargeIsCreated()
        {
            BankSlipRequest bankSlipRequest = CreateBankSlip();
            ChargeByBankSlipRequest chargeRequest = CreateChargeByBankSlipRequest(bankSlipRequest);

            ChargeByBankSlipResponse result = await Client
               .ForCharge()
               .WithBankSlip()
               .Load(chargeRequest)
               .ChargeAsync();

            await Task.Delay(1000);
            ChargeByBankSlipResponse chargeByBankSlipResponse = await Client
                .ForCharge()
                .WithBankSlip()
                .GetByIdAsync(result.Id!);
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(
                chargeRequest,
                options => options.Excluding(c => c.PaymentMethod!.BankSlip!.DueDate));
            result.PaymentMethod!.BankSlip!.DueDate.Should().Be(bankSlipRequest.DueDate.Date);
            result.Id.Should().StartWith("CHAR");
            result.CreatedDate.Date.Should().Be(DateTime.UtcNow.Date);
            result.Links.Should().NotBeNullOrEmpty();
            result.Amount!.Summary!.Paid.Should().Be(0);
            result.Amount.Summary.Total.Should().Be(1000);
            result.Amount.Summary.Refunded.Should().Be(0);
            result.PaymentResponse!.Message.Should().Be("SUCESSO");
            result.PaymentResponse.Code.Should().Be(20000);
            result.Should().BeEquivalentTo(chargeByBankSlipResponse);
        }

        private ChargeByBankSlipRequest CreateChargeByBankSlipRequest(BankSlipRequest bankSlipRequest)
        {
            return Client
                .ForCharge()
                .WithBankSlip()
                .AddBankSlip(bankSlipRequest)
                .WithAmount(new ChargeAmountRequest
                {
                    Value = 1000,
                    Currency = "BRL"
                })
                .WithReferenceId("ex-00001")
                .WithDescription("Motivo do pagamento")
                .WithNotificationUrl("https://myurl.com")
                .Build();
        }

        private BankSlipRequest CreateBankSlip()
        {
            var holderAddress = Fixture.Build<Address>()
                .With(h => h.Number, "1384")
                .With(h => h.Locality, "Pinheiros")
                .With(h => h.City, "Sao Paulo")
                .With(h => h.Region, "Sao Paulo")
                .With(h => h.RegionCode, "SP")
                .With(h => h.Country, "Brasil")
                .With(h => h.PostalCode, "01452002")
                .Create();
            var holder = Fixture.Build<BankSlipHolder>()
                .With(h => h.Address, holderAddress)
                .With(h => h.Email, "email@teste.com")
                .With(h => h.TaxId, "12345678909")
                .Create();
            return Fixture.Build<BankSlipRequest>()
                .With(b => b.DueDate, DateTime.Now.AddYears(1))
                .With(b => b.Holder, holder)
                .Create();
        }
    }
}
