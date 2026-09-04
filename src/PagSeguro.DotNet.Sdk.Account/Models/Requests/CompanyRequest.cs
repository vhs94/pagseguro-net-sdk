using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Account.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Account.Models.Requests
{
    /// <summary>
    /// Dados cadastrais da empresa enviados na criação da conta.
    /// <see href="https://developer.pagbank.com.br/reference/criar-conta">ler documentação</see>
    /// </summary>
    public class CompanyRequest : CompanyBase
    {
        /// <summary>
        /// Razão Social da empresa. De 3 a 144 caracteres.
        /// </summary>
        [JsonPropertyName("name")]
        public string? CompanyName { get; set; }
        /// <summary>
        /// Endereço da empresa.
        /// </summary>
        public Address? Address { get; set; }
    }
}
