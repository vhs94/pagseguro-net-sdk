using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses
{
    /// <summary>
    /// Pagamento de uma fatura de assinatura.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-pagamento-1">ler documentação</see>
    /// </summary>
    public class SubscriptionPaymentResponse
    {
        /// <summary>Identificador do pagamento. Por exemplo, PAYM_123.</summary>
        public string? Id { get; set; }

        /// <summary>Fatura paga.</summary>
        public InvoiceReference? Invoice { get; set; }

        /// <summary>Situação do pagamento. Por exemplo, PAID e UNPAID.</summary>
        public string? Status { get; set; }

        /// <summary>Assinante cobrado.</summary>
        public CustomerReference? Customer { get; set; }

        /// <summary>Meio de pagamento utilizado.</summary>
        [JsonPropertyName("payment_method")]
        public SubscriptionPaymentMethod? PaymentMethod { get; set; }

        /// <summary>Resposta do adquirente.</summary>
        public PaymentProvider? Provider { get; set; }

        /// <summary>Data e horário de criação do pagamento.</summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedDate { get; set; }

        /// <summary>Data e horário da última alteração do pagamento.</summary>
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedDate { get; set; }

        /// <summary>Links relacionados ao pagamento.</summary>
        public ICollection<SubscriptionLink> Links { get; set; } = [];
    }
}
