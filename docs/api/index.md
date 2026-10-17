# Referência da API

Esta seção é gerada automaticamente a partir dos comentários XML do código, então
ela acompanha o SDK a cada versão.

Use o menu à esquerda para navegar pelos namespaces, ou a busca no topo para
achar um tipo pelo nome.

## Por onde começar

| Quero... | Comece por |
| --- | --- |
| Entender o ponto de entrada | [IPagSeguroClient](PagSeguro.DotNet.Sdk.IPagSeguroClient.yml) |
| Configurar credenciais | [ClientSettings](PagSeguro.DotNet.Sdk.Settings.ClientSettings.yml) |
| Criar e pagar pedidos | [IOrderProvider](PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders.IOrderProvider.yml) |
| Cobrar com cartão de crédito | [ICreditCardChargeProvider](PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods.ICreditCardChargeProvider.yml) |
| Usar a página de pagamento hospedada | [ICheckoutProvider](PagSeguro.DotNet.Sdk.Checkout.Interfaces.ICheckoutProvider.yml) |
| Montar cobrança recorrente | [ISubscriptionProvider](PagSeguro.DotNet.Sdk.Subscriptions.Interfaces.ISubscriptionProvider.yml) |
| Simular taxas e parcelamento | [IFeeProvider](PagSeguro.DotNet.Sdk.Orders.Interfaces.Fees.IFeeProvider.yml) |
| Autenticar via Connect | [IAuthorizationProvider](PagSeguro.DotNet.Sdk.Connect.Interfaces.IAuthorizationProvider.yml) |
| Tratar erros da API | [PagSeguroHttpException](PagSeguro.DotNet.Sdk.Common.Exceptions.Http.PagSeguroHttpException.yml) |

> [!NOTE]
> A documentação cobre as **interfaces** dos providers, que são o contrato do SDK.
> Você não instancia as classes concretas: elas são resolvidas pelo client
> (`client.ForOrder()`, `client.ForPlan()`, ...).
