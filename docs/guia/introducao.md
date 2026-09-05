# Introdução

## Instalação

```sh
dotnet add package PagSeguro.DotNet.Sdk
```

O pacote tem como alvo o **.NET 10** e traz junto todos os módulos
(pedidos, cobranças, checkout, assinaturas, Connect, conta e chaves).

## Criando o client

### Injeção de dependência (ASP.NET)

Registre o client uma vez na inicialização e receba `IPagSeguroClient` por injeção:

```csharp
builder.Services.AddPagSeguro(options =>
{
    options.ClientId = "seu-client-id";
    options.ClientSecret = "seu-client-secret";
    options.Token = "seu-token";
    options.Environment = PagSeguroEnvironment.Sandbox;
});
```

```csharp
public class PagamentoService(IPagSeguroClient client)
{
    public Task<OrderResponse> ConsultarAsync(string orderId)
        => client.ForOrder().GetByIdAsync(orderId);
}
```

### Instanciando direto

```csharp
using var client = new PagSeguroClient(new ClientSettings
{
    ClientId = "seu-client-id",
    ClientSecret = "seu-client-secret",
    Token = "seu-token",
    Environment = PagSeguroEnvironment.Sandbox
});
```

> [!IMPORTANT]
> `PagSeguroClient` implementa `IDisposable` e `IAsyncDisposable`. Ele registra um
> cliente HTTP próprio, então crie **uma instância e reaproveite** durante a vida da
> aplicação, descartando-a no fim. Criar um client por requisição vaza um `HttpClient`
> a cada chamada.

## O ponto de entrada

Tudo parte de `IPagSeguroClient`. Cada serviço tem o seu provider:

```csharp
client.ForOrder()                   // pedidos
client.ForCharge()                  // cobranças avulsas
client.ForCheckout()                // página de pagamento hospedada
client.ForPlan()                    // planos de assinatura
client.ForCustomer()                // assinantes
client.ForSubscription()            // assinaturas
client.ForCoupon()                  // cupons de desconto
client.ForInvoice()                 // faturas
client.ForSubscriptionPayment()     // pagamentos e estornos
client.ForSubscriptionPreference()  // preferências e chave pública
client.ForFee()                     // simulação de taxas
client.ForAuthorization()           // OAuth2
client.ForApplication()             // aplicações Connect
client.ForAccount()                 // contas
client.ForPublicKey()               // chaves públicas
client.ForCertificate()             // certificado digital
```

## Primeira chamada

Simular as taxas de uma transação não exige autorização do usuário, então é o
jeito mais rápido de validar que as credenciais funcionam:

```csharp
FeeResponse taxas = await client
    .ForFee()
    .WithValue(10000)          // R$ 100,00 em centavos
    .WithMaxInstallments(12)
    .CalculateAsync();
```

> [!TIP]
> Todos os valores monetários da API do PagBank são **inteiros em centavos**.
> R$ 1.500,99 é enviado como `150099`.
