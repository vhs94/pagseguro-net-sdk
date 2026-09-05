using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers
{
    public class OrderSearchIntegrationTests : BaseIntegrationTests
    {
        [Fact]
        public async Task GetByChargeIdAsync_ChargeBelongsToAnOrder_OrderIsReturned()
        {
            ChargedOrderResponse<ChargeByCreditCardResponse> createdOrder = await CreateOrderWithChargeAsync();
            string chargeId = createdOrder.Charges.First().Id!;

            ICollection<OrderResponse> result = await Client
                .ForOrder()
                .GetByChargeIdAsync(chargeId);

            result.Should().NotBeNullOrEmpty();
            result.Should().ContainSingle();
            result.First().Id.Should().Be(createdOrder.Id);
            result.First().ReferenceId.Should().Be(createdOrder.ReferenceId);
            result.First().Items.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetByChargeIdAsync_ChargeDoesNotBelongToAnOrder_ListIsEmpty()
        {
            // Cobrança avulsa: criada direto em /charges, sem pedido associado.
            ChargeByCreditCardResponse charge = await Client
                .ForCharge()
                .WithCreditCard()
                .AddPaymentMethod(CreateCreditCardPaymentMethod())
                .WithAmount(new ChargeAmountRequest { Currency = "BRL", Value = 1000 })
                .WithReferenceId("ex-00001")
                .WithDescription("Cobranca avulsa")
                .ChargeAsync();

            ICollection<OrderResponse> result = await Client
                .ForOrder()
                .GetByChargeIdAsync(charge.Id!);

            result.Should().BeEmpty();
        }

        private async Task<ChargedOrderResponse<ChargeByCreditCardResponse>> CreateOrderWithChargeAsync()
        {
            ChargeByCreditCardRequest charge = Client
                .ForCharge()
                .WithCreditCard()
                .AddPaymentMethod(CreateCreditCardPaymentMethod())
                .WithAmount(new ChargeAmountRequest { Currency = "BRL", Value = 1000 })
                .WithReferenceId("ex-00001")
                .WithDescription("Motivo do pagamento")
                .Build();

            return await Client
                .ForOrder()
                .WithReferenceId("ex-00001")
                .WithCustomer(new Customer
                {
                    Name = "Jose da Silva",
                    Email = "jose@test.com",
                    TaxId = "12345678909"
                })
                .WithItem(new ItemRequest
                {
                    ReferenceId = "item-00001",
                    Name = "Produto de teste",
                    Quantity = 1,
                    UnitAmount = 1000
                })
                .WithCreditCard()
                .AddCharge(charge)
                .CreateAsync();
        }

        private static CreditCardPaymentMethodRequest CreateCreditCardPaymentMethod()
        {
            return new CreditCardPaymentMethodRequest
            {
                Installments = 1,
                Capture = true,
                SoftDescriptor = "testeloja",
                Card = new CardRequest
                {
                    Number = "4539620659922097",
                    ExpMonth = 12,
                    ExpYear = 2026,
                    SecurityCode = 123,
                    Holder = new Holder { Name = "Jose da Silva" }
                }
            };
        }
    }
}
