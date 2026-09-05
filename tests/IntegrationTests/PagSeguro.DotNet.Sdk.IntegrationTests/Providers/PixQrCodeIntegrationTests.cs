using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers
{
    /// <summary>
    /// Fluxo Pix do PagBank: o Pix é solicitado pelos qr_codes do pedido, e não por
    /// um payment_method PIX na cobrança.
    /// </summary>
    /// <remarks>
    /// Uma cobrança com payment_method.type PIX exige uma chave Pix cadastrada na
    /// conta e é recusada no sandbox com "Pix key was not found for pix payment
    /// method", por isso não há teste para esse caminho.
    /// </remarks>
    public class PixQrCodeIntegrationTests : BaseIntegrationTests
    {
        [Fact]
        public async Task CreateAsync_OrderHasQrCode_QrCodeIsGenerated()
        {
            OrderResponse result = await CreateOrderWithQrCodeAsync();

            result.Should().NotBeNull();
            result.Id.Should().StartWith("ORDE_");
            result.QrCodes.Should().ContainSingle();

            QrCodeResponse qrCode = result.QrCodes.First();
            qrCode.Id.Should().StartWith("QRCO_");
            qrCode.Amount!.Value.Should().Be(1000);

            // O "copia e cola" é o que o comprador realmente usa para pagar:
            // sem ele o QR Code não serve para nada.
            qrCode.Text.Should().NotBeNullOrEmpty();
            qrCode.Text.Should().StartWith("0002");

            qrCode.Links.Should().NotBeNullOrEmpty();
            qrCode.Links.Should().Contain(link => link.Rel == "QRCODE.PNG");
            qrCode.Links.Should().Contain(link => link.Rel == "QRCODE.BASE64");
        }

        [Fact]
        public async Task GetByIdAsync_OrderHasQrCode_QrCodeIsReturned()
        {
            OrderResponse created = await CreateOrderWithQrCodeAsync();

            OrderResponse result = await Client
                .ForOrder()
                .GetByIdAsync(created.Id!);

            result.Id.Should().Be(created.Id);
            result.QrCodes.Should().ContainSingle();
            result.QrCodes.First().Id.Should().Be(created.QrCodes.First().Id);
            result.QrCodes.First().Text.Should().Be(created.QrCodes.First().Text);
        }

        private async Task<OrderResponse> CreateOrderWithQrCodeAsync()
        {
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
                .WithQrCode(new QrCodeRequest
                {
                    Amount = new QrCodeAmount { Value = 1000 }
                })
                .CreateAsync();
        }
    }
}
