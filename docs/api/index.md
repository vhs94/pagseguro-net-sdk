# Referência da API

Esta seção é gerada automaticamente a partir dos comentários XML do código, então
ela acompanha o SDK a cada versão.

Use o menu à esquerda para navegar pelos namespaces, ou a busca no topo para
achar um tipo pelo nome.

## Por onde começar

| Quero... | Comece por |
| --- | --- |
| Entender o ponto de entrada | <xref:PagSeguro.DotNet.Sdk.IPagSeguroClient> |
| Configurar credenciais | <xref:PagSeguro.DotNet.Sdk.Settings.ClientSettings> |
| Criar e pagar pedidos | <xref:PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders.IOrderProvider> |
| Cobrar com cartao de credito | <xref:PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods.ICreditCardChargeProvider> |
| Usar a pagina de pagamento hospedada | <xref:PagSeguro.DotNet.Sdk.Checkout.Interfaces.ICheckoutProvider> |
| Montar cobranca recorrente | <xref:PagSeguro.DotNet.Sdk.Subscriptions.Interfaces.ISubscriptionProvider> |
| Simular taxas e parcelamento | <xref:PagSeguro.DotNet.Sdk.Orders.Interfaces.Fees.IFeeProvider> |
| Autenticar via Connect | <xref:PagSeguro.DotNet.Sdk.Connect.Interfaces.IAuthorizationProvider> |
| Tratar erros da API | <xref:PagSeguro.DotNet.Sdk.Common.Exceptions.Http.PagSeguroHttpException> |

> [!NOTE]
> A documentacao cobre as **interfaces** dos providers, que sao o contrato do SDK.
> Voce nunca instancia as classes concretas: elas chegam prontas pelo client
> (`client.ForOrder()`, `client.ForPlan()`, ...).
