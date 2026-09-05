using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>Portador do cartão usado na assinatura.</summary>
    public class CardHolder
    {
        /// <summary>Nome do portador.</summary>
        public string? Name { get; set; }

        /// <summary>Data de nascimento do portador, no formato AAAA-MM-DD.</summary>
        [JsonPropertyName("birth_date")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BirthDate { get; set; }

        /// <summary>Documento (CPF/CNPJ) do portador.</summary>
        [JsonPropertyName("tax_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TaxId { get; set; }

        /// <summary>Telefone do portador.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CustomerPhone? Phone { get; set; }
    }
}
