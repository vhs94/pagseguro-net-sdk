# Contas, aplicações Connect e chaves públicas

Este guia cobre os recursos de conta e configuração que complementam os fluxos de
[pagamento](pedidos-e-cobrancas.md) e [autorização Connect](autenticacao.md).

## Contas PagBank

Use `ForAccount()` para criar uma conta e consultar uma conta pelo identificador:

```csharp
CreatedAccountResponse criada = await client.ForAccount().CreateAsync(
	new AccountRequest
	{
		Type = "BUYER",
		Email = "cliente@exemplo.com",
		Person = new Person
		{
			Name = "Nome do titular",
			BirthDate = new DateTime(1990, 1, 1),
			TaxId = "12345678909",
			Address = new Address
			{
				Street = "Rua Exemplo",
				Number = "100",
				Locality = "Centro",
				City = "Sao Paulo",
				RegionCode = "SP",
				Country = "BRA",
				PostalCode = "01001000"
			},
			Phones =
			[
				new Phone { Country = "55", Area = "11", Number = "999999999" }
			]
		},
		TosAcceptance = new TosAcceptanceRequest
		{
			UserIp = ipDoUsuario,
			Date = dataRealDoAceite
		}
	});

AccountResponse consultada = await client.ForAccount().GetByIdAsync(criada.Id!);
```

`Type` pode ser `BUYER`, `SELLER` ou `ENTERPRISE`. Para `SELLER` e
`ENTERPRISE`, `BusinessCategory` e os dados `Company` são obrigatórios. Os campos
exigidos variam conforme o tipo de conta e as regras atuais do PagBank; consulte
a [referência oficial de criação de conta](https://developer.pagbank.com.br/reference/criar-conta)
antes de enviar dados reais. Envie o IP e a data do aceite efetivamente coletados
do titular, nunca valores fixos de exemplo.

A resposta da criação contém um token em `criada.Token`. Trate-o como uma
credencial: não o registre em logs, não o exponha ao navegador e armazene-o em
um cofre de segredos. O processo de criação também exige as credenciais e o
estado de autenticação descritos em [Autenticação](autenticacao.md).

## Aplicações Connect

Uma aplicação Connect representa a integração que solicita autorização de um
vendedor. Cadastre a URL de redirecionamento que sua aplicação realmente usa:

```csharp
ApplicationResponse aplicacao = await client.ForApplication().CreateAsync(
	new ApplicationRequest
	{
		Name = "Minha integração",
		Description = "Integração de pagamentos da loja",
		Site = "https://loja.exemplo.com",
		RedirectUrl = "https://loja.exemplo.com/connect/callback"
	});

ApplicationResponse detalhes = await client
	.ForApplication()
	.GetByClientIdAsync(aplicacao.ClientId!);
```

A resposta inclui `ClientId` e `ClientSecret`. Configure-os no client antes de
iniciar Connect:

```csharp
client.ConfigureClientApplication(aplicacao.ClientId!, aplicacao.ClientSecret!);
```

O `ClientSecret` é confidencial e deve permanecer no servidor. Não o inclua em
aplicativos móveis, JavaScript entregue ao navegador, repositórios ou logs. Para
a troca do código de autorização, renovação e revogação de tokens, veja
[Connect (OAuth2)](autenticacao.md#connect-oauth2).

## Chave pública para cartões

A chave pública da conta permite que o navegador criptografe os dados do cartão
antes de enviá-los ao seu servidor. Consulte a chave atual ou crie uma quando a
conta ainda não tiver uma:

```csharp
PublicKeyResponse chave = await client.ForPublicKey().GetAsync();

// Se ainda não houver chave, crie-a:
PublicKeyResponse novaChave = await client.ForPublicKey().CreateAsync();
```

Use `PublicKey` no fluxo de criptografia do front-end e envie ao back-end o valor
criptografado para `CardTokenRequest.Encrypted` ou para o campo equivalente da
cobrança. O SDK recebe o dado já criptografado; ele não oferece uma biblioteca
JavaScript de criptografia. Evite transmitir ou persistir número, validade e CVV
em claro. Para cartões armazenados, consulte [Cartões e 3DS](cartoes-e-3ds.md).

A rotação é feita com `UpdateAsync()`:

```csharp
PublicKeyResponse rotacionada = await client.ForPublicKey().UpdateAsync();
```

A chave anterior permanece válida por até **7 dias** após a rotação. Atualize o
front-end para usar a nova chave e migre os fluxos de criptografia durante esse
período, conforme a [documentação oficial de alteração de chave](https://developer.pagbank.com.br/reference/alterar-chave-publica).

> [!IMPORTANT]
> A chave das cobranças recorrentes é outra chave, gerenciada por
> `ForSubscriptionPreference()`. A API de Assinaturas invalida a chave recorrente
> anterior quando uma nova é criada. Não confunda esse ciclo com a rotação da
> chave pública de cartões desta seção; veja o guia de [Assinaturas](assinaturas.md).
