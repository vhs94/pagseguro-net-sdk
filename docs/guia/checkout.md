# Checkout

O checkout é a **página de pagamento hospedada pelo PagBank**. Você monta os
itens, recebe um link e manda o comprador para lá — sem lidar com dados de cartão.

## Criando

```csharp
CheckoutResponse checkout = await client.ForCheckout().CreateAsync(new CheckoutRequest
{
    ReferenceId = "pedido-00001",
    CustomerModifiable = true,
    Items =
    [
        new CheckoutItem
        {
            ReferenceId = "item-00001",
            Name = "Produto",
            Quantity = 1,
            UnitAmount = 1000
        }
    ],
    PaymentMethods =
    [
        new CheckoutPaymentMethod(CheckoutPaymentMethodType.CreditCard),
        new CheckoutPaymentMethod(CheckoutPaymentMethodType.Pix)
    ],
    RedirectUrl = "https://seusite.com/obrigado",
    NotificationUrls = ["https://seusite.com/webhook"]
});

string linkDePagamento = checkout.Links
    .First(link => link.Rel == "PAY").Href!;
```

Mande o comprador para `linkDePagamento`.

> [!NOTE]
> Quando `CustomerModifiable` é `false`, o objeto `Customer` passa a ser obrigatório.

## Consultando e alterando o status

```csharp
CheckoutResponse atual = await client.ForCheckout().GetByIdAsync(checkout.Id!);

await client.ForCheckout().InactivateAsync(checkout.Id!);  // para de aceitar pagamentos
await client.ForCheckout().ActivateAsync(checkout.Id!);    // volta a aceitar
```
