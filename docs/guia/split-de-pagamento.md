# Split de pagamento

O split divide o valor de uma cobrança entre várias contas PagBank. Serve para
marketplaces, plataformas e qualquer cenário em que o dinheiro de uma venda
precisa ser repartido entre mais de um vendedor.

> [!IMPORTANT]
> O split só funciona se a conta principal estiver **habilitada como marketplace**
> pelo PagBank e se as contas recebedoras já existirem e estiverem aprovadas.
> Sem isso a API recusa a cobrança com `invalid_id` no recebedor.

## Dividindo uma cobrança

O objeto `Splits` fica na **cobrança**, não no pedido:

```csharp
ChargeByCreditCardRequest cobranca = client
    .ForCharge()
    .WithCreditCard()
    .AddPaymentMethod(pagamento)
    .WithAmount(new ChargeAmountRequest { Value = 5000, Currency = "BRL" })
    .WithDescription("Venda com split")
    .Build();

cobranca.Splits = new SplitRequest
{
    Method = SplitMethod.Fixed,
    Receivers =
    [
        new SplitReceiverRequest
        {
            Account = new SplitAccount { Id = "ACCO_..." },
            Amount = new SplitAmount { Value = 1000 },
            Reason = "Comissão do parceiro"
        }
    ]
};

ChargeByCreditCardResponse resultado = await client
    .ForCharge()
    .WithCreditCard()
    .Load(cobranca)
    .ChargeAsync();
```

`Method` aceita dois valores, disponíveis em `SplitMethod`:

| Valor | Como o `Amount.Value` é lido |
| --- | --- |
| `SplitMethod.Fixed` | Valor absoluto, em centavos |
| `SplitMethod.Percentage` | Percentual do total da cobrança |

O split também vale para boleto (`ChargeByBankSlipRequest.Splits`).

## Custódia

Você pode reter o valor devido a um recebedor em vez de repassá-lo na hora:

```csharp
new SplitReceiverRequest
{
    Account = new SplitAccount { Id = "ACCO_..." },
    Amount = new SplitAmount { Value = 1000 },
    Configurations = new SplitReceiverConfigurations
    {
        Custody = new SplitCustody
        {
            Apply = true,
            Release = new SplitCustodyRelease
            {
                // liberação automática; no máximo 365 dias
                Scheduled = DateTimeOffset.Now.AddDays(30)
            }
        }
    }
}
```

Sem `Release.Scheduled`, a liberação precisa ser feita manualmente.

## Consultando a divisão

```csharp
SplitResponse split = await client.ForSplit().GetByIdAsync("SPLI_...");

foreach (SplitReceiverResponse recebedor in split.Receivers)
{
    Console.WriteLine($"{recebedor.Type}: {recebedor.Amount!.Value}");
}
```

`Type` vem como `PRIMARY` (a conta dona da transação) ou `SECONDARY`.

## Liberando a custódia

Só a conta dona da transação pode liberar:

```csharp
await client.ForSplit().ReleaseCustodyAsync("SPLI_...", new SplitCustodyReleaseRequest
{
    Receivers =
    [
        new SplitCustodyReceiverRequest
        {
            Account = new SplitAccount { Id = "ACCO_..." }
        }
    ]
});
```

> [!NOTE]
> O endpoint aceita no máximo 500 chamadas a cada 5 minutos.

## No sandbox

As credenciais compartilhadas de sandbox **não** são de uma conta marketplace, então
não há como concluir uma divisão de verdade por lá. O que dá para validar é que o
objeto está sendo enviado corretamente: a API responde
`invalid_id ... receivers.account.id`, ou seja, ela leu o split e só não encontrou o
recebedor. É exatamente isso que os testes de integração do SDK verificam.
