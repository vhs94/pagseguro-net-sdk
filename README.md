# PagSeguro .NET SDK

Cliente tipado para integrar aplicações .NET às APIs do PagBank (PagSeguro). Requer .NET 10.

[![NuGet](https://img.shields.io/nuget/vpre/PagSeguro.DotNet.Sdk.svg)](https://www.nuget.org/packages/PagSeguro.DotNet.Sdk)
[![codecov](https://codecov.io/gh/vhs94/pagseguro-net-sdk/branch/main/graph/badge.svg?token=DBC135AXUC)](https://codecov.io/gh/vhs94/pagseguro-net-sdk)
[![Documentação](https://img.shields.io/badge/docs-GitHub%20Pages-blue)](https://vhs94.github.io/pagseguro-net-sdk/)


## Instalação

```sh
dotnet add package PagSeguro.DotNet.Sdk
```

No Package Manager Console do Visual Studio:

```powershell
Install-Package PagSeguro.DotNet.Sdk
```

## Documentação

Guias de integração e referência completa: **[vhs94.github.io/pagseguro-net-sdk](https://vhs94.github.io/pagseguro-net-sdk/)**

- [Introdução e configuração](https://vhs94.github.io/pagseguro-net-sdk/guia/introducao.html)
- [Autenticação e Connect](https://vhs94.github.io/pagseguro-net-sdk/guia/autenticacao.html)
- [Contas, aplicações e chaves](https://vhs94.github.io/pagseguro-net-sdk/guia/contas-aplicacoes-e-chaves.html)
- [Pedidos e cobranças](https://vhs94.github.io/pagseguro-net-sdk/guia/pedidos-e-cobrancas.html)
- [Checkout hospedado](https://vhs94.github.io/pagseguro-net-sdk/guia/checkout.html)
- [Cartões e 3DS](https://vhs94.github.io/pagseguro-net-sdk/guia/cartoes-e-3ds.html)
- [Split de pagamento](https://vhs94.github.io/pagseguro-net-sdk/guia/split-de-pagamento.html)
- [Assinaturas](https://vhs94.github.io/pagseguro-net-sdk/guia/assinaturas.html)
- [Notificações e operação em produção](https://vhs94.github.io/pagseguro-net-sdk/guia/notificacoes-e-producao.html)
- [Erros, sandbox e testes](https://vhs94.github.io/pagseguro-net-sdk/guia/erros.html)
- [Referência da API](https://vhs94.github.io/pagseguro-net-sdk/api/)

## Como usar

Em uma aplicação ASP.NET, registre o client uma vez e injete `IPagSeguroClient`
onde for necessário. Carregue as credenciais de uma configuração segura, como
variáveis de ambiente, User Secrets ou um cofre de segredos; não as versione.

```csharp
builder.Services.AddPagSeguro(options =>
{
    options.ClientId = builder.Configuration["PagBank:ClientId"];
    options.ClientSecret = builder.Configuration["PagBank:ClientSecret"];
    options.Token = builder.Configuration["PagBank:Token"];
    options.Environment = PagSeguroEnvironment.Sandbox;
});
```

Use o provider correspondente à operação. Exemplo de consulta de pedido:

```csharp
public sealed class PedidosService(IPagSeguroClient client)
{
    public Task<OrderResponse> ConsultarAsync(string orderId)
        => client.ForOrder().GetByIdAsync(orderId);
}
```

Para criar pedidos, cobranças, checkout, cartões, split ou assinaturas, siga o
guia específico antes de integrar em produção. Os exemplos e avisos de segurança
estão na [documentação completa](https://vhs94.github.io/pagseguro-net-sdk/).

## Desenvolvimento e testes

Clone o repositório e execute os testes com .NET 10:

```sh
dotnet test
```

Os testes de integração acessam o sandbox e podem exigir credenciais; consulte
[Ambientes e testes](https://vhs94.github.io/pagseguro-net-sdk/guia/ambientes-e-testes.html).

> [!NOTE]
> Este é um projeto open source mantido pela comunidade e sem vínculo oficial com o PagBank.

