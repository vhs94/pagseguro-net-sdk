using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Models.Shared
{
    /// <summary>
    /// Dados cadastrais da empresa, compartilhados entre a criação e a consulta da conta.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-account">ler documentação</see>
    /// </summary>
    public abstract class CompanyBase
    {
        /// <summary>
        /// CNPJ da empresa.
        /// </summary>
        [JsonPropertyName("tax_id")]
        public string? TaxId { get; set; }
        /// <summary>
        /// Nome fantasia da empresa.
        /// </summary>
        [JsonPropertyName("trade_name")]
        public string? TradeName { get; set; }
        /// <summary>
        /// Lista de telefones da empresa.
        /// </summary>
        public ICollection<Phone> Phones { get; set; }

        public CompanyBase() => Phones = [];
    }
}
