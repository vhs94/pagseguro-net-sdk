Client moderno das APIs do PagSeguro

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
