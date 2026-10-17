using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Portador do cartão informado na tokenização.
    /// <see href="https://developer.pagbank.com.br/reference/validar-armanezar-cartao-pagbank">ler documentação</see>
    /// </summary>
    public class CardTokenHolder
    {
        /// <summary>Nome do portador, como impresso no cartão.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        /// <summary>CPF ou CNPJ do portador, somente dígitos.</summary>
        [JsonPropertyName("tax_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TaxId { get; set; }
    }
}
