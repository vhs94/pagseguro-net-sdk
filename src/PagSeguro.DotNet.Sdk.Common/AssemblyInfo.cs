using System.Runtime.CompilerServices;

// Os testes precisam enxergar a infraestrutura protegida dos providers
// (URL base e validações) sem que ela fique visível para quem consome o pacote.
[assembly: InternalsVisibleTo("PagSeguro.DotNet.Sdk.Common.Tests")]
