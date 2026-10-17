# Notificações e operação em produção

Notificações permitem que o PagBank avise sua aplicação sobre mudanças de estado.
Elas são callbacks HTTP enviados à URL que você informa ao criar o recurso. O SDK
ajuda a configurar essas URLs e consultar recursos, mas não hospeda o endpoint que
recebe as notificações.

## Configurando URLs de notificação

### Pedidos e cobranças

Informe a URL no builder do pedido:

```csharp
OrderResponse pedido = await client
	.ForOrder()
	.WithReferenceId("pedido-00001")
	.WithNotificationUrl("https://api.exemplo.com/webhooks/pagbank/pedidos")
	.WithCustomer(cliente)
	.WithItem(item)
	.WithQrCode(new QrCodeRequest
	{
		Amount = new QrCodeAmount { Value = 1000 }
	})
	.CreateAsync();
```

Para uma cobrança avulsa, os builders de cobrança também oferecem
`WithNotificationUrl(...)` e `WithNotificationUrls(...)`. A URL é enviada com a
cobrança; consulte a referência de `ForCharge()` para o método de pagamento
escolhido.

### Checkout hospedado

`CheckoutRequest` permite configurar URLs para mudanças do checkout e do
pagamento. A URL de notificação de pagamento é separada da URL geral do checkout:

```csharp
CheckoutResponse checkout = await client.ForCheckout().CreateAsync(new CheckoutRequest
{
	ReferenceId = "checkout-00001",
	Items = [itemCheckout],
	PaymentMethods = [new CheckoutPaymentMethod(CheckoutPaymentMethodType.Pix)],
	NotificationUrls = ["https://api.exemplo.com/webhooks/pagbank/checkouts"],
	PaymentNotificationUrls = ["https://api.exemplo.com/webhooks/pagbank/pagamentos"]
});
```

### Assinaturas

As URLs de notificação de assinatura são uma preferência da conta, não uma
configuração individual de cada assinatura. Consulte a configuração existente e
atualize-a:

```csharp
NotificationPreferenceResponse preferencias = await client
	.ForSubscriptionPreference()
	.GetNotificationPreferencesAsync();

NotificationPreferenceResponse atualizadas = await client
	.ForSubscriptionPreference()
	.UpdateNotificationPreferencesAsync(new NotificationPreferenceRequest
	{
		Urls = ["https://api.exemplo.com/webhooks/pagbank/assinaturas"]
	});
```

A atualização envia as preferências fornecidas. Se sua integração também altera
preferências de e-mail, leia o valor atual e preserve os campos que deseja manter
em `NotificationPreferenceRequest.Email`.

## Receba, enfileire e reconcilie

Ao implementar o endpoint de callback na sua aplicação:

1. Disponibilize uma URL HTTPS pública e configure-a no recurso ou na preferência
   correspondente.
2. Valide a autenticidade da notificação de acordo com o mecanismo e os cabeçalhos
   documentados pelo PagBank para aquele produto. Não trate uma requisição apenas
   por conhecer a URL como prova de que ela é legítima.
3. Persista ou coloque o evento em uma fila durável e responda prontamente conforme
   o contrato do PagBank. Faça o processamento mais demorado fora do ciclo HTTP do
   callback.
4. Projete o processamento para receber o mesmo evento mais de uma vez e para
   eventos chegarem fora de ordem. Use uma chave de evento/recurso como deduplicação,
   quando disponível, e não aplique a mesma transição duas vezes.
5. Use a notificação para iniciar a reconciliação; consulte o recurso na API antes
   de atualizar o estado financeiro definitivo da sua aplicação.

O SDK **não** inclui um servidor de webhook, middleware de validação de assinatura
ou modelo genérico de evento. A estrutura dos eventos e o mecanismo de validação
podem variar entre produtos; consulte a documentação oficial do endpoint que você
está integrando e não presuma um formato comum.

Para consultar o estado atual com o SDK, use o identificador PagBank salvo quando
o recurso foi criado:

```csharp
OrderResponse pedidoAtual = await client.ForOrder().GetByIdAsync(pedido.Id!);
ChargeByCreditCardResponse cobrancaAtual = await client
	.ForCharge()
	.WithCreditCard()
	.GetByIdAsync(chargeId);
```

Os estados e operações disponíveis dependem do recurso e do meio de pagamento.
Veja [Pedidos e cobranças](pedidos-e-cobrancas.md), [Checkout](checkout.md) e
[Assinaturas](assinaturas.md).

## Evite duplicar operações ao repetir chamadas

O SDK envia uma chave `x-idempotency-key` em diversos fluxos de criação e alteração
de pedidos, cobranças e assinaturas. A chave é gerada internamente para cada
chamada e não há um parâmetro público para fornecer ou reutilizar uma chave sua.
Portanto, chamar novamente um método de criação após um timeout pode enviar uma
chave diferente e criar outro recurso.

Mantenha no seu sistema um identificador de negócio único (por exemplo,
`ReferenceId`), guarde os IDs PagBank retornados e, se o resultado de uma chamada
ficar incerto, consulte/reconcilie o recurso antes de tentar criá-lo novamente.
Não configure uma política automática de repetição de `POST` sem considerar essa
limitação.

> [!TIP]
> `ReferenceId` ajuda a relacionar a operação com seu pedido interno, mas não
> substitui uma chave de idempotência reutilizável nem impede, por si só, que a API
> crie duas operações.
