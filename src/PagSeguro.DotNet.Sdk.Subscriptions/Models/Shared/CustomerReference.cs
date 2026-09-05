using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>
    /// Referência ao assinante. Informe apenas o Id para reaproveitar um assinante
    /// existente, ou os dados completos para criar um novo junto com a assinatura.
    /// </summary>
    public class CustomerReference : CustomerBase
    {
        /// <summary>Identificador do assinante. Por exemplo, CUST_123.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Id { get; set; }

        /// <summary>Meios de pagamento do assinante, ao criá-lo junto com a assinatura.</summary>
        [JsonPropertyName("billing_info")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<BillingInfo>? BillingInfo { get; set; }
    }
}
