using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses
{
    /// <summary>
    /// Fatura de uma assinatura.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-fatura">ler documentação</see>
    /// </summary>
    public class InvoiceResponse
    {
        /// <summary>Identificador da fatura. Por exemplo, INVO_123.</summary>
        public string? Id { get; set; }

        /// <summary>Valor total da fatura.</summary>
        public Money? Amount { get; set; }

        /// <summary>Situação da fatura. Por exemplo, PAID, WAITING e UNPAID.</summary>
        public string? Status { get; set; }

        /// <summary>Plano que originou a fatura.</summary>
        public PlanReference? Plan { get; set; }

        /// <summary>Itens que compõem o valor da fatura.</summary>
        public ICollection<InvoiceItem> Items { get; set; } = [];

        /// <summary>Assinatura que originou a fatura.</summary>
        public SubscriptionReference? Subscription { get; set; }

        /// <summary>Número da ocorrência do ciclo faturado.</summary>
        public int Occurrence { get; set; }

        /// <summary>Assinante cobrado.</summary>
        public CustomerReference? Customer { get; set; }

        /// <summary>Data e horário de criação da fatura.</summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedDate { get; set; }

        /// <summary>Data e horário da última alteração da fatura.</summary>
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedDate { get; set; }

        /// <summary>Links relacionados à fatura.</summary>
        public ICollection<SubscriptionLink> Links { get; set; } = [];
    }
}
