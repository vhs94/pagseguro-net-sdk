---
_layout: landing
---

# PagSeguro .NET SDK

Client moderno e tipado para as APIs do PagBank (PagSeguro), escrito em .NET 10.

[![NuGet](https://img.shields.io/nuget/vpre/PagSeguro.DotNet.Sdk.svg)](https://www.nuget.org/packages/PagSeguro.DotNet.Sdk)
[![codecov](https://codecov.io/gh/vhs94/pagseguro-net-sdk/branch/main/graph/badge.svg?token=DBC135AXUC)](https://codecov.io/gh/vhs94/pagseguro-net-sdk)

```sh
dotnet add package PagSeguro.DotNet.Sdk
```

## O que dá para fazer

| Serviço | Provider | O que cobre |
| --- | --- | --- |
| Pedidos | `ForOrder()` | Criar, consultar, pagar e buscar pedidos, inclusive Pix por QR Code |
| Cobranças | `ForCharge()` | Cartão de crédito, cartão de débito com 3DS e boleto |
| Checkout | `ForCheckout()` | Página de pagamento hospedada pelo PagBank |
| Assinaturas | `ForPlan()`, `ForCustomer()`, `ForSubscription()` | Cobrança recorrente completa |
| Cupons e faturas | `ForCoupon()`, `ForInvoice()`, `ForSubscriptionPayment()` | Descontos, faturas, pagamentos e estornos |
| Taxas | `ForFee()` | Simulação de taxas e parcelamento |
| Connect | `ForAuthorization()`, `ForApplication()` | OAuth2: autorizar, renovar e revogar tokens |
| Conta e chaves | `ForAccount()`, `ForPublicKey()`, `ForCertificate()` | Cadastro, chave pública e certificado digital |

## Comece por aqui

- [Introdução](guia/introducao.md) — instalação e primeiros passos
- [Autenticação](guia/autenticacao.md) — credenciais, Connect e certificado digital
- [Pedidos e cobranças](guia/pedidos-e-cobrancas.md) — o fluxo mais comum
- [Assinaturas](guia/assinaturas.md) — cobrança recorrente
- [Referência da API](api/index.md) — todas as classes e membros

> [!NOTE]
> Projeto open source mantido pela comunidade, sem vínculo oficial com o PagBank.
