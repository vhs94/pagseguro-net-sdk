using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests
{
    /// <summary>
    /// Dados enviados para criar uma assinatura.
    /// <see href="https://developer.pagbank.com.br/reference/criar-assinatura">ler documentação</see>
    /// </summary>
    public class SubscriptionRequest
    {
        /// <summary>Identificador próprio atribuído à assinatura. Máximo de 65 caracteres.</summary>
        [JsonPropertyName("reference_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ReferenceId { get; set; }

        /// <summary>Plano que define o valor e a periodicidade da assinatura.</summary>
        public PlanReference? Plan { get; set; }

        /// <summary>
        /// Assinante da cobrança recorrente. Informe apenas o Id para reaproveitar
        /// um assinante existente.
        /// </summary>
        public CustomerReference? Customer { get; set; }

        /// <summary>Meios de pagamento usados na cobrança recorrente.</summary>
        [JsonPropertyName("payment_method")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<SubscriptionPaymentMethod>? PaymentMethod { get; set; }

        /// <summary>Indica se o valor da primeira fatura é proporcional ao período restante.</summary>
        [JsonPropertyName("pro_rata")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? ProRata { get; set; }

        /// <summary>Melhor dia para o faturamento.</summary>
        [JsonPropertyName("best_invoice_date")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public BestInvoiceDate? BestInvoiceDate { get; set; }

        /// <summary>Data da próxima fatura.</summary>
        [JsonPropertyName("next_invoice_at")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? NextInvoiceAt { get; set; }

        /// <summary>Valor da assinatura, quando diferente do valor do plano.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Money? Amount { get; set; }
    }
}
