using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests
{
    /// <summary>
    /// Dados enviados para alterar uma assinatura.
    /// <see href="https://developer.pagbank.com.br/reference/alterar-assinatura">ler documentação</see>
    /// </summary>
    public class SubscriptionUpdateRequest
    {
        /// <summary>Indica se a alteração é cobrada proporcionalmente.</summary>
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

        /// <summary>Novo valor da assinatura.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Money? Amount { get; set; }

        /// <summary>Novo plano da assinatura.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PlanReference? Plan { get; set; }
    }
}
