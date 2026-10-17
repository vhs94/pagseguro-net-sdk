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
| Cartões e 3DS | `ForCardToken()`, `ForAuthenticationSession()` | Armazenar cartões e abrir a sessão de autenticação |
| Split | `ForSplit()` | Divisão do pagamento entre contas e liberação da custódia |
| Assinaturas | `ForPlan()`, `ForCustomer()`, `ForSubscription()` | Cobrança recorrente completa |
| Cupons e faturas | `ForCoupon()`, `ForInvoice()`, `ForSubscriptionPayment()` | Descontos, faturas, pagamentos e estornos |
| Taxas | `ForFee()` | Simulação de taxas e parcelamento |
| Connect | `ForAuthorization()`, `ForApplication()` | OAuth2: autorizar (inclusive por SMS), renovar e revogar tokens |
| Conta e chaves | `ForAccount()`, `ForPublicKey()`, `ForCertificate()` | Cadastro, chave pública e certificado digital |

## Comece por aqui

- [Introdução](guia/introducao.md) — instalação, configuração e primeiros passos
- [Autenticação](guia/autenticacao.md) — credenciais, Connect e certificado digital
- [Contas, aplicações e chaves](guia/contas-aplicacoes-e-chaves.md) — onboarding e chaves públicas
- [Pedidos e cobranças](guia/pedidos-e-cobrancas.md) — pedidos, meios de pagamento e taxas
- [Checkout](guia/checkout.md) — página de pagamento hospedada
- [Cartões e 3DS](guia/cartoes-e-3ds.md) — tokenização e autenticação do portador
- [Split de pagamento](guia/split-de-pagamento.md) — dividir uma venda entre contas
- [Assinaturas](guia/assinaturas.md) — cobrança recorrente, cupons e faturas
- [Notificações e produção](guia/notificacoes-e-producao.md) — callbacks, reconciliação e repetição segura
- [Tratamento de erros](guia/erros.md) — exceções e respostas da API
- [Ambientes e testes](guia/ambientes-e-testes.md) — sandbox e testes
- [Referência da API](api/index.md) — tipos e membros públicos do SDK

> [!NOTE]
> Projeto open source mantido pela comunidade, sem vínculo oficial com o PagBank.
