using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Connect.Models.Shared
{
    /// <summary>
    /// Dados comuns de uma aplicação Connect, compartilhados entre a criação e a consulta.
    /// <see href="https://developer.pagbank.com.br/reference/criar-aplicacao">ler documentação</see>
    /// </summary>
    public abstract class ApplicationBase
    {
        /// <summary>
        /// Nome da aplicação.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Descrição da aplicação.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Endereço do site da aplicação.
        /// </summary>
        public string? Site { get; set; }
        /// <summary>
        /// URL para a qual o usuário é redirecionado após conceder a autorização.
        /// </summary>
        [JsonPropertyName("redirect_uri")]
        public string? RedirectUrl { get; set; }
    }
}
