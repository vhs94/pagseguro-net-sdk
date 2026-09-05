# Ambientes e testes

## Escolhendo o ambiente

```csharp
options.Environment = PagSeguroEnvironment.Sandbox;    // testes
options.Environment = PagSeguroEnvironment.Production; // produção
```

O SDK resolve sozinho a URL de cada serviço, inclusive o host separado das
assinaturas (`api.assinaturas.pagseguro.com`).

## Cartões de teste

Cartões publicados pelo PagBank para o sandbox:

| Bandeira | Número | Resultado |
| --- | --- | --- |
| Visa | `4539620659922097` | Aprovado |
| Visa | `4929291898380766` | Negado |
| Mastercard | `5240082975622454` | Aprovado |
| Mastercard | `5530062640663264` | Negado |
| Elo | `4514161122113757` | Aprovado |
| Hipercard | `6062828598919021` | Aprovado |

Validade `12/2026` e CVV `123` (Amex usa 4 dígitos).

## Limitações conhecidas do sandbox

> [!WARNING]
> Estas são limitações do ambiente de testes do PagBank, não do SDK:
>
> - **Cartão de débito não é habilitado.** A autenticação 3DS sempre volta
>   `NOT_AUTHENTICATED` e a cobrança é recusada com `20017`, mesmo com o cartão
>   de aprovação documentado.
> - **Pix como meio de pagamento da cobrança** exige uma chave Pix cadastrada na
>   conta. Use os QR Codes do pedido.
> - **`payment_instructions`** (multa, juros e desconto no boleto) é aceito, mas
>   descartado: não volta na resposta nem na consulta.
> - **A API de assinaturas aplica rate limit agressivo.** Rodar muitos testes de
>   integração em paralelo devolve `429`.

## Rodando os testes do projeto

```sh
dotnet test                                              # tudo
dotnet test --filter "FullyQualifiedName!~IntegrationTests"   # só unitários
```

Os testes de integração batem na API real de sandbox e rodam em série de
propósito — em paralelo eles estouram o rate limit.
