using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>
    /// Dados comuns de um assinante.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-assinante">ler documentação</see>
    /// </summary>
    public abstract class CustomerBase
    {
        /// <summary>Identificador próprio atribuído ao assinante. Máximo de 65 caracteres.</summary>
        [JsonPropertyName("reference_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ReferenceId { get; set; }

        /// <summary>Nome completo do assinante. Máximo de 150 caracteres.</summary>
        public string? Name { get; set; }

        /// <summary>E-mail do assinante. Precisa ser diferente do e-mail do vendedor.</summary>
        public string? Email { get; set; }

        /// <summary>CPF (11 dígitos) ou CNPJ (14 dígitos) do assinante, apenas números.</summary>
        [JsonPropertyName("tax_id")]
        public string? TaxId { get; set; }

        /// <summary>Data de nascimento do assinante, no formato AAAA-MM-DD.</summary>
        [JsonPropertyName("birth_date")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BirthDate { get; set; }

        /// <summary>Telefones do assinante. Ao menos um é obrigatório.</summary>
        public ICollection<CustomerPhone>? Phones { get; set; }

        /// <summary>Endereço do assinante.</summary>
        public CustomerAddress? Address { get; set; }
    }
}
