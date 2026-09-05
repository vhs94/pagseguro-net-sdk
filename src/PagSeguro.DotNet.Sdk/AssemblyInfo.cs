using System.Runtime.CompilerServices;

// Os testes verificam que o access_token e o desafio são guardados após a
// autenticação, sem expor as credenciais para quem consome o pacote.
[assembly: InternalsVisibleTo("PagSeguro.DotNet.Sdk.Tests")]
