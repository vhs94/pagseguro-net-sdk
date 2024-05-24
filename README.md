Client moderno para APIs PagSeguro


[![Version](https://img.shields.io/nuget/vpre/PagSeguro.DotNet.Sdk.svg)](https://www.nuget.org/packages/PagSeguro.DotNet.Sdk)
[![codecov](https://codecov.io/gh/vhs94/pagseguro-net-sdk/branch/main/graph/badge.svg?token=DBC135AXUC)](https://codecov.io/gh/vhs94/pagseguro-net-sdk)


## Install

* **Package Manager Console (Visual Studio)**
```sh
Install-Package PagSeguro.DotNet.Sdk
```

* **dotnet cli**
```sh
dotnet add package PagSeguro.DotNet.Sdk
```

# Como usar

### ASP.NET

No ASP.NET, use o middleware para injetar uma instância do ```IPagseguroClient```

```csharp
//configuracao credenciais pagseguro
builder.Services.AddPagSeguro(options =>
{
    options.ClientId = "8815a5e2-616b-4754-a6d1-40d92b71674c";
    options.ClientSecret = "45e5a4de-b8eb-4b78-9ce6-fad416b1953c";
    options.Token = "BCABE5E7AA9D43BCBBB76E3C45C1567A";
    options.Environment = PagSeguroEnvironment.Sandbox;
});
```

### ConsoleApp

Crie uma instância de PagSeguroClient, caso queira usar o ambiente de Produção, o Environment pode ser omitido.

```csharp
var client = new PagSeguroClient(new ClientSettings
{
    Environment = PagSeguroEnvironment.Sandbox,
    Token = "<SEU_TOKEN>"
});
```
Use as fluent interfaces para manipular as APIs, para opções disponíveis [veja a Wiki.](https://github.com/vhs94/pagseguro-net-sdk/wiki)

```csharp
var creditCardOrder = await client
    .ForOrder()
    .WithReferenceId("ref-id")
    .WithNotificationUrl("https://my.url")
    .WithCreditCard()
    .AddCharge(new ChargeByCreditCardWriteDto())
    .CreateAsync();
```
# Unit Testing

Compatível com Mocking frameworks como NSubstitute ou Moq. Devido ao design usando fluent interfaces, proporciona alta testabilidade.

```csharp
    [Fact]
    public async Task ForOrder_GetByIdAsync_GetByIdAsyncIsCalledWithId()
    {
        var clientMock = Substitute.For<PagSeguroClient>(new ClientSettings());
        string orderId = "order-id";

        await clientMock
            .ForOrder()
            .GetByIdAsync(orderId);

        await clientMock
            .Received(1)
            .ForOrder()
            .GetByIdAsync(orderId);
    }
```

