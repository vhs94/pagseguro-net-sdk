using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests
{
    /// <summary>
    /// Dados cadastrais alteráveis de um assinante.
    /// <see href="https://developer.pagbank.com.br/reference/alterar-dados-cadastrais-do-assinante">ler documentação</see>
    /// </summary>
    /// <remarks>
    /// Não inclui tax_id nem billing_info de propósito: a API recusa a alteração do
    /// documento ("It is not possible to change the tax_id") e o meio de pagamento é
    /// alterado por <c>UpdateBillingInfoAsync</c>.
    /// </remarks>
    public class CustomerUpdateRequest
    {
        /// <summary>Identificador próprio atribuído ao assinante.</summary>
        [JsonPropertyName("reference_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ReferenceId { get; set; }

        /// <summary>Nome completo do assinante.</summary>
        public string? Name { get; set; }

        /// <summary>E-mail do assinante.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Email { get; set; }

        /// <summary>Data de nascimento do assinante, no formato AAAA-MM-DD.</summary>
        [JsonPropertyName("birth_date")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BirthDate { get; set; }

        /// <summary>Telefones do assinante.</summary>
        public ICollection<CustomerPhone>? Phones { get; set; }

        /// <summary>Endereço do assinante.</summary>
        public CustomerAddress? Address { get; set; }
    }
}
