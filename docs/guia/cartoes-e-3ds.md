# Cartões armazenados e autenticação 3DS

## Armazenando um cartão

`ForCardToken()` valida o cartão e o guarda no PagBank, devolvendo um identificador
que substitui os dados do cartão nas cobranças seguintes. Assim o número não
precisa trafegar nem ficar guardado na sua aplicação.

```csharp
CardTokenResponse cartao = await client.ForCardToken().CreateAsync(new CardTokenRequest
{
    Encrypted = cartaoCriptografadoNoNavegador,
    Holder = new CardTokenHolder { Name = "Jose da Silva", TaxId = "12345678909" }
});

Console.WriteLine(cartao.Id);          // CARD_...
Console.WriteLine(cartao.Brand);       // visa
Console.WriteLine(cartao.LastDigits);  // 1111
```

O caminho recomendado é o `Encrypted`: o cartão é criptografado no navegador com a
chave pública da conta (`ForPublicKey()`), e a sua aplicação nunca vê o número.

> [!WARNING]
> Enviar `Number`, `ExpMonth`, `ExpYear` e `SecurityCode` abertos só é permitido
> para integrações **certificadas PCI**.

```csharp
// somente para integrações certificadas PCI
CardTokenResponse cartao = await client.ForCardToken().CreateAsync(new CardTokenRequest
{
    Number = "4111111111111111",
    ExpMonth = "12",
    ExpYear = "2030",
    SecurityCode = "123",
    Holder = new CardTokenHolder { Name = "Jose da Silva" }
});
```

## Sessão de autenticação 3DS

A autenticação 3DS acontece no navegador, conduzida pelo SDK de front-end do
PagBank. O seu back-end participa só do primeiro passo: criar a sessão.

```csharp
AuthenticationSessionResponse sessao = await client
    .ForAuthenticationSession()
    .CreateAsync();

// repasse sessao.Session para o front-end
```

O fluxo completo:

1. O back-end cria a sessão e devolve `Session` para o front-end.
2. O SDK de front-end autentica o portador com a sessão.
3. O front-end devolve um `authentication_method.id`.
4. O back-end cria a cobrança informando esse id em `AuthenticationMethodRequest`.

```csharp
CreditCardWith3DsAuthPaymentMethodRequest pagamento = new()
{
    Installments = 1,
    Capture = true,
    Card = cartao,
    AuthenticationMethod = new AuthenticationMethodRequest
    {
        Type = "THREEDS",
        Id = idVindoDoFrontEnd
    }
};
```

> [!NOTE]
> A sessão vale por **30 minutos**. `ExpiresAt` vem em milissegundos desde a época
> Unix — use `DateTimeOffset.FromUnixTimeMilliseconds(sessao.ExpiresAt)`.

Este endpoint fica em um host próprio (`sdk.pagseguro.com`), separado do resto da
API. O SDK cuida disso automaticamente.
