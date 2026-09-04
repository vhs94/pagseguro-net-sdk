using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Connect.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Connect.Models.Responses
{
    /// <summary>
    /// Dados de uma aplicação Connect retornados pela API.
    /// <see href="https://developer.pagbank.com.br/reference/criar-aplicacao">ler documentação</see>
    /// </summary>
    public class ApplicationResponse : ApplicationBase
    {
        /// <summary>
        /// Identificador público da aplicação, usado para iniciar o fluxo de autorização.
        /// </summary>
        [JsonPropertyName("client_id")]
        public string? ClientId { get; set; }
        /// <summary>
        /// Chave secreta da aplicação.
        /// Deve ser mantida em segurança e nunca exposta no lado do cliente.
        /// </summary>
        [JsonPropertyName("client_secret")]
        public string? ClientSecret { get; set; }
        /// <summary>
        /// Identificador da conta PagBank dona da aplicação.
        /// </summary>
        [JsonPropertyName("account_id")]
        public string? AccountId { get; set; }
        /// <summary>
        /// Tipo da aplicação.
        /// </summary>
        [JsonPropertyName("client_type")]
        public string? ClientType { get; set; }
    }
}
