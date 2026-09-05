# Autenticação

## Credenciais

`ClientSettings` reúne tudo que o SDK precisa:

| Propriedade | Para que serve |
| --- | --- |
| `Token` | Token da conta, usado como Bearer na maioria das chamadas |
| `ClientId` / `ClientSecret` | Identificam a sua aplicação Connect |
| `PrivateKey` | Chave privada usada para decriptar o desafio do certificado digital |
| `Environment` | `Sandbox` ou `Production` |

> [!WARNING]
> Nunca versione essas credenciais. Use variáveis de ambiente, *user secrets* ou um
> cofre de segredos.

## Connect (OAuth2)

Para agir em nome de outro vendedor, troque o código de autorização por um
`access_token`. O client guarda o token e passa a usá-lo nas chamadas seguintes:

```csharp
client.ConfigureClientApplication("seu-client-id", "seu-client-secret");

AuthorizationCodeResponse token = await client.ConnectAsync(new AuthorizationCodeRequest
{
    Code = "codigo-recebido-no-redirect",
    RedirectUri = "https://seusite.com/callback",
    Scope = ApiScopes.CreatePayments | ApiScopes.ReadPayments
});
```

### Renovando o token

O `access_token` expira. Use o `refresh_token` devolvido na autorização:

```csharp
AuthorizationCodeResponse novo = await client.RefreshAccessTokenAsync(new RefreshTokenRequest
{
    RefreshToken = token.RefreshToken
});
```

> [!NOTE]
> Cada renovação gera um **novo** `refresh_token` e invalida o anterior. Guarde
> sempre o mais recente.

### Revogando

```csharp
await client.ForAuthorization().RevokeTokenAsync(new RevokeTokenRequest
{
    Token = token.RefreshToken,
    TokenTypeHint = TokenTypeHint.RefreshToken
});
```

Revogar o `refresh_token` invalida também o `access_token` associado.

## Certificado digital

O certificado usa o fluxo de desafio. O SDK decripta o desafio com a sua
`PrivateKey` automaticamente:

```csharp
await client.ConnectChallengeAsync();

CertificateResponse certificado = await client.ForCertificate().CreateAsync();
```
