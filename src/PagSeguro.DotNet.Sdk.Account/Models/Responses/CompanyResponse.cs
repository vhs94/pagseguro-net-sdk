using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Account.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Account.Models.Responses
{
    /// <summary>
    /// Dados cadastrais da empresa retornados na consulta da conta.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-conta">ler documentação</see>
    /// </summary>
    public class CompanyResponse : CompanyBase
    {
        /// <summary>
        /// Razão Social da empresa.
        /// </summary>
        [JsonPropertyName("company_name")]
        public string? CompanyName { get; set; }
        /// <summary>
        /// Endereços cadastrados para a empresa.
        /// </summary>
        public ICollection<Address> Address { get; set; }

        public CompanyResponse() => Address = [];
    }
}
