# Tratamento de erros

Toda resposta de erro da API vira uma exceção derivada de
`PagSeguroHttpException`, que carrega o corpo cru da resposta em `Response` e o
status em `StatusCode`.

| Exceção | Status | Quando acontece |
| --- | --- | --- |
| `BadRequestException` | 400 | Payload inválido — o motivo está em `Response` |
| `UnauthorizedException` | 401 | Token ausente ou inválido |
| `ForbiddenException` | 403 | Sem acesso ao recurso |
| `NotFoundException` | 404 | Recurso inexistente |
| `NotAcceptableException` | 406 | Verbo HTTP incorreto |
| `ConflictException` | 409 | Conflito, por exemplo chave de idempotência repetida |
| `TooManyRequestsException` | 429 | Rate limit — tente de novo mais tarde |
| `InternalServerErrorException` | 500 | Erro do lado do PagBank |
| `UnknownHttpException` | outros | Status não mapeado |

```csharp
try
{
    await client.ForCharge().WithCreditCard().Load(cobranca).ChargeAsync();
}
catch (BadRequestException ex)
{
    // ex.Response traz o error_messages do PagBank, com parameter_name e description
    logger.LogWarning("Recusado pelo PagBank: {Corpo}", ex.Response);
}
catch (TooManyRequestsException)
{
    // aplique backoff e tente novamente
}
```

> [!TIP]
> A mensagem da exceção é genérica de propósito. O detalhe útil — qual campo foi
> recusado e por quê — está sempre em `Response`.

## Erros de validação locais

Antes de chamar a API, o SDK valida o que já dá para validar e lança:

| Exceção | Significado |
| --- | --- |
| `ClientNotConnectedException` | Falta `access_token`: chame `ConnectAsync()` |
| `ClientNotConnectedWithChallengeException` | Falta o desafio: chame `ConnectChallengeAsync()` |
| `MissingClientApplicationException` | Falta `ClientId`/`ClientSecret` |
| `PrivateKeyNotFoundException` | Falta a chave privada nas configurações |
