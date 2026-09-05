# Assinaturas

A cobrança recorrente do PagBank roda em um **host próprio**
(`api.assinaturas.pagseguro.com`). O SDK cuida disso: use os providers
normalmente que a URL certa é escolhida sozinha.

O fluxo é sempre **plano → assinante → assinatura**.

## 1. Plano

Define quanto e de quanto em quanto tempo cobrar:

```csharp
PlanResponse plano = await client.ForPlan().CreateAsync(new PlanRequest
{
    ReferenceId = "plano-premium",
    Name = "Plano Premium",
    Description = "Acesso completo",
    Amount = new Money { Value = 1990, Currency = "BRL" },
    Interval = new PlanInterval { Unit = "MONTH", Length = 1 },
    Trial = new PlanTrial { Enabled = true, Days = 7 },
    PaymentMethod = ["CREDIT_CARD"]
});
```

> [!TIP]
> Deixe `SetupFee` nulo quando não houver taxa de adesão. A API recusa `0`.

Ciclo de vida:

```csharp
await client.ForPlan().InactivateAsync(plano.Id!);  // não aceita novas assinaturas
await client.ForPlan().ActivateAsync(plano.Id!);
PlanListResponse pagina = await client.ForPlan().ListAsync(offset: 0, limit: 20);
```

## 2. Assinante

O cartão é tokenizado pelo PagBank e nunca volta em claro:

```csharp
CustomerResponse assinante = await client.ForCustomer().CreateAsync(new CustomerRequest
{
    ReferenceId = "cliente-00001",
    Name = "Jose da Silva",
    Email = "jose@exemplo.com",
    TaxId = "12345678909",
    BirthDate = "1990-01-01",
    Phones = [new CustomerPhone { Country = "55", Area = "11", Number = "999999999" }],
    Address = new CustomerAddress
    {
        Street = "Avenida Brigadeiro Faria Lima",
        Number = "1384",
        Locality = "Pinheiros",
        City = "Sao Paulo",
        RegionCode = "SP",
        Country = "BRA",
        PostalCode = "01452002"
    },
    BillingInfo =
    [
        new BillingInfo
        {
            Type = "CREDIT_CARD",
            Card = new SubscriptionCard
            {
                Number = "4539620659922097",
                ExpMonth = "12",
                ExpYear = "2030",
                SecurityCode = "123",
                Holder = new CardHolder { Name = "Jose da Silva" }
            }
        }
    ]
});
```

> [!IMPORTANT]
> A API aceita **um assinante por CPF/CNPJ**. Repetir o `TaxId` devolve erro.
> O documento também **não pode ser alterado** depois: por isso
> `UpdateAsync` recebe `CustomerUpdateRequest`, que não expõe `TaxId` nem
> `BillingInfo`.

Alterando dados e meio de pagamento:

```csharp
await client.ForCustomer().UpdateAsync(assinante.Id!, new CustomerUpdateRequest
{
    Name = "Jose da Silva Junior",
    Phones = assinante.Phones,
    Address = assinante.Address
});

await client.ForCustomer().UpdateBillingInfoAsync(assinante.Id!, new BillingInfoRequest
{
    Type = "CREDIT_CARD",
    Card = novoCartao
});
```

## 3. Assinatura

```csharp
SubscriptionResponse assinatura = await client.ForSubscription().CreateAsync(
    new SubscriptionRequest
    {
        ReferenceId = "assinatura-00001",
        Plan = new PlanReference { Id = plano.Id },
        Customer = new CustomerReference { Id = assinante.Id },
        PaymentMethod =
        [
            new SubscriptionPaymentMethod
            {
                Type = "CREDIT_CARD",
                Card = new SubscriptionCard { SecurityCode = "123" }
            }
        ]
    });
```

Ciclo de vida:

```csharp
await client.ForSubscription().SuspendAsync(assinatura.Id!);
await client.ForSubscription().ActivateAsync(assinatura.Id!);
await client.ForSubscription().CancelAsync(assinatura.Id!);
await client.ForSubscription().RetryAsync(assinatura.Id!);  // nova tentativa manual
```

## Faturas, pagamentos e estornos

```csharp
InvoiceListResponse faturas = await client
    .ForSubscription()
    .ListInvoicesAsync(assinatura.Id!);

InvoiceResponse fatura = faturas.Invoices.First();

SubscriptionPaymentListResponse pagamentos = await client
    .ForInvoice()
    .ListPaymentsAsync(fatura.Id!);

RefundResponse estorno = await client
    .ForSubscriptionPayment()
    .RefundAsync(pagamentos.Payments.First().Id!, new RefundRequest
    {
        Amount = new Money { Value = 1990, Currency = "BRL" }
    });
```

## Cupons de desconto

```csharp
CouponResponse cupom = await client.ForCoupon().CreateAsync(new CouponRequest
{
    Name = "BEMVINDO",
    Discount = new CouponDiscount { Type = "PERCENT", Value = 10 },
    Duration = new CouponDuration { Type = "REPEATING", Occurrences = 3 },
    RedemptionLimit = 100,
    ExpiresAt = "2027-01-01"
});
```

> [!NOTE]
> `Discount.Type` aceita apenas `PERCENT` ou `AMOUNT`, e `Duration` é
> obrigatório — mesmo que a documentação oficial sugira outra coisa.

## Preferências de notificação

```csharp
await client.ForSubscriptionPreference().UpdateNotificationPreferencesAsync(
    new NotificationPreferenceRequest
    {
        Urls = ["https://seusite.com/webhook-assinaturas"],
        Email = new NotificationEmailPreference
        {
            Merchant = new NotificationEmailTarget { Enabled = true },
            Customer = new NotificationEmailTarget { Enabled = false }
        }
    });
```
