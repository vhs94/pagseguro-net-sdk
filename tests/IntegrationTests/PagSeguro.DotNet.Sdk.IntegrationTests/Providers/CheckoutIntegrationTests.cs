using FluentAssertions;
using PagSeguro.DotNet.Sdk.Checkout.Models.Requests;
using PagSeguro.DotNet.Sdk.Checkout.Models.Responses;
using PagSeguro.DotNet.Sdk.Checkout.Models.Shared;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers
{
    public class CheckoutIntegrationTests : BaseIntegrationTests
    {
        private const string ValidUrl = "https://myurl.com";

        [Fact]
        public async Task CreateAsync_RequestIsValid_CheckoutIsCreated()
        {
            CheckoutRequest checkoutRequest = CreateCheckoutRequest();

            CheckoutResponse result = await Client
                .ForCheckout()
                .CreateAsync(checkoutRequest);

            result.Should().NotBeNull();
            result.Id.Should().StartWith("CHEC_");
            result.Status.Should().Be("ACTIVE");
            // created_at chega com o offset -03:00 e o System.Text.Json converte
            // para o horario LOCAL da maquina. Comparar com DateTime.UtcNow.Date
            // confrontaria uma data local com uma data UTC, o que falha sempre que
            // os dois lados caem em dias diferentes. A janela abaixo compara o
            // instante, nao o dia, e ainda verifica que o recurso foi criado agora.
            result.CreatedDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMinutes(10));
            result.ReferenceId.Should().Be(checkoutRequest.ReferenceId);
            result.Items.Should().BeEquivalentTo(checkoutRequest.Items);
            result.PaymentMethods.Should().BeEquivalentTo(checkoutRequest.PaymentMethods);
            result.RedirectUrl.Should().Be(ValidUrl);
            result.NotificationUrls.Should().Contain(ValidUrl);

            // A relação PAY é o endereço da página de pagamento entregue ao comprador:
            // sem ela o checkout não tem serventia.
            result.Links.Should().Contain(link => link.Rel == "PAY");
            result.Links.Single(link => link.Rel == "PAY").Href.Should().NotBeNullOrEmpty();
            result.Links.Should().Contain(link => link.Rel == "SELF");
        }

        [Fact]
        public async Task GetByIdAsync_CheckoutExists_CheckoutIsReturned()
        {
            CheckoutResponse created = await Client
                .ForCheckout()
                .CreateAsync(CreateCheckoutRequest());

            CheckoutResponse result = await Client
                .ForCheckout()
                .GetByIdAsync(created.Id!);

            result.Should().NotBeNull();
            result.Id.Should().Be(created.Id);
            result.Status.Should().Be("ACTIVE");
            result.ReferenceId.Should().Be(created.ReferenceId);
            result.Items.Should().BeEquivalentTo(created.Items);
        }

        [Fact]
        public async Task InactivateAsync_CheckoutIsActive_CheckoutIsInactivated()
        {
            CheckoutResponse created = await Client
                .ForCheckout()
                .CreateAsync(CreateCheckoutRequest());
            created.Status.Should().Be("ACTIVE");

            CheckoutResponse result = await Client
                .ForCheckout()
                .InactivateAsync(created.Id!);

            result.Id.Should().Be(created.Id);
            result.Status.Should().Be("INACTIVE");

            CheckoutResponse reloaded = await Client
                .ForCheckout()
                .GetByIdAsync(created.Id!);
            reloaded.Status.Should().Be("INACTIVE");
        }

        [Fact]
        public async Task ActivateAsync_CheckoutIsInactive_CheckoutIsActivated()
        {
            CheckoutResponse created = await Client
                .ForCheckout()
                .CreateAsync(CreateCheckoutRequest());
            await Client.ForCheckout().InactivateAsync(created.Id!);

            CheckoutResponse result = await Client
                .ForCheckout()
                .ActivateAsync(created.Id!);

            result.Id.Should().Be(created.Id);
            result.Status.Should().Be("ACTIVE");

            CheckoutResponse reloaded = await Client
                .ForCheckout()
                .GetByIdAsync(created.Id!);
            reloaded.Status.Should().Be("ACTIVE");
        }

        private static CheckoutRequest CreateCheckoutRequest()
        {
            return new CheckoutRequest
            {
                ReferenceId = "ex-00001",
                CustomerModifiable = true,
                Items =
                [
                    new CheckoutItem
                    {
                        ReferenceId = "item-00001",
                        Name = "Produto de teste",
                        Description = "Item usado no teste de integração",
                        Quantity = 1,
                        UnitAmount = 1000
                    }
                ],
                PaymentMethods =
                [
                    new CheckoutPaymentMethod(CheckoutPaymentMethodType.CreditCard),
                    new CheckoutPaymentMethod(CheckoutPaymentMethodType.Pix)
                ],
                RedirectUrl = ValidUrl,
                NotificationUrls = [ValidUrl]
            };
        }
    }
}
