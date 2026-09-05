using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses
{
    /// <summary>
    /// Assinatura retornada pela API.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-assinatura">ler documentação</see>
    /// </summary>
    public class SubscriptionResponse
    {
        /// <summary>Identificador da assinatura. Por exemplo, SUBS_123.</summary>
        public string? Id { get; set; }

        /// <summary>Identificador próprio atribuído à assinatura.</summary>
        [JsonPropertyName("reference_id")]
        public string? ReferenceId { get; set; }

        /// <summary>
        /// Situação da assinatura. Valores possíveis: ACTIVE, OVERDUE, SUSPENDED,
        /// PENDING e CANCELED.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>Valor cobrado a cada ciclo.</summary>
        public Money? Amount { get; set; }

        /// <summary>Plano da assinatura.</summary>
        public PlanReference? Plan { get; set; }

        /// <summary>Assinante da cobrança recorrente.</summary>
        public CustomerReference? Customer { get; set; }

        /// <summary>Meios de pagamento da assinatura.</summary>
        [JsonPropertyName("payment_method")]
        public ICollection<SubscriptionPaymentMethod> PaymentMethod { get; set; } = [];

        /// <summary>Data da próxima fatura.</summary>
        [JsonPropertyName("next_invoice_at")]
        public DateTime? NextInvoiceAt { get; set; }

        /// <summary>Ciclo de faturamento corrente.</summary>
        [JsonPropertyName("billing_cycle")]
        public BillingCycle? BillingCycle { get; set; }

        /// <summary>Indica se a cobrança é proporcional ao período.</summary>
        [JsonPropertyName("pro_rata")]
        public bool ProRata { get; set; }

        /// <summary>Indica se a divisão de pagamento está habilitada.</summary>
        [JsonPropertyName("split_enabled")]
        public bool SplitEnabled { get; set; }

        /// <summary>Data e horário de criação da assinatura.</summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedDate { get; set; }

        /// <summary>Data e horário da última alteração da assinatura.</summary>
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Cupom de desconto aplicado à assinatura, quando houver. Continua
        /// preenchido no ciclo corrente mesmo depois de removido: a remoção só
        /// vale a partir da próxima recorrência.
        /// </summary>
        public CouponReference? Coupon { get; set; }

        /// <summary>Links relacionados à assinatura.</summary>
        public ICollection<SubscriptionLink> Links { get; set; } = [];
    }
}
