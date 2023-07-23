Client moderno das APIs do PagSeguro
# Install

```sh
dotnet add package PagSeguro.DotNet.Sdk 
```

# Como usar

- Crie uma instância de PagSeguroClient, caso queira usar o ambiente de Produção, o Environment pode ser omitido.
```csharp
var client = new PagSeguroClient(new ClientSettings
{
    Environment = PagSeguroEnvironment.Sandbox,
    Token = "<SEU_TOKEN>",
});
```
- Use os providers disponiveis para manipular as APIs

```csharp
var application = await client.Application.GetApplicationAsync("123");
```

## Autenticação
O Client já faz a gestão da autenticação, realizando descriptografia de challenge inclusive. Podemos trabalhar de duas formas:

### Connect AuthorizationCode
Este tipo de conexão é o mais comum

- Crie e conecte a instância
```csharp
var client = new PagSeguroClient(new ClientSettings
{
    Token = "<SEU_TOKEN>",
});

await client.ConnectAsync(new AuthorizationCodeWriteDto
{
    ClientId = "<SEU_CLIENT_ID>",
    ClientSecret = "<SEU_CLIENT_SECRET>",
    Code = "<CODIGO_REDIRECIONADO_URL>",
    RedirectUri = "<URL_REDIRECIONAMENTO>",
    Scope = ApiScopes.ReadAccounts //se omitido, utiliza todos os scopes
});
```
- Com o client conectado, agora é possível utilizar as APIs autenticadas

```csharp
var account = await client.Account.GetAccountByIdAsync("accountId");
```

### Connect Challenge

O Challenge é usado apenas no fluxo de criação de certificados digitais.

- Crie a instância e solicite o challenge

```csharp
var client = new PagSeguroClient(new ClientSettings
{
    Token = "<SEU_TOKEN>",
    PrivateKey = "<SUA_PRIVATE_KEY>" // private key usada para descriptografar o challenge
});

await client.ConnectChallengeAsync(new ChallengeWriteDto
{
    ClientId = "<SEU_CLIENT_ID>",
    ClientSecret = "<SEU_CLIENT_SECRET>"
});
```

- Com o client conectado,agora é possível criar certificados

```csharp
var certificate = await client.DigitalCertificate.CreateCertificateAsync();
```

# Fluxos Disponíveis

- Authorization
- Application
- Account
- DigitalCertificate
- PublicKey

# Tests
Todo o projeto está com alto nível de coverage, estou trabalhando para exibir isso no repo.

# Roadmap

- Disponibilizar mais fluxos
- Disponibilizar middlewares para a utilização com Dependency Injection
- Integration Tests com sandbox para alertar possíveis mudanças feitas pelo time do pagseguro
