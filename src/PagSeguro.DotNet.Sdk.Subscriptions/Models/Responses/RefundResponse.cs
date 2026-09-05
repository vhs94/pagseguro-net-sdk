using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses
{
    /// <summary>
    /// Estorno de um pagamento de assinatura.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-estorno">ler documentação</see>
    /// </summary>
    public class RefundResponse
    {
        /// <summary>Identificador do estorno.</summary>
        public string? Id { get; set; }

        /// <summary>Valor estornado.</summary>
        public Money? Amount { get; set; }

        /// <summary>Pagamento que foi estornado.</summary>
        public PaymentReference? Payment { get; set; }

        /// <summary>Situação do estorno. Por exemplo, SUCCESS.</summary>
        public string? Status { get; set; }

        /// <summary>Tipo do estorno. Por exemplo, FULL.</summary>
        public string? Type { get; set; }

        /// <summary>Data e horário de criação do estorno.</summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedDate { get; set; }

        /// <summary>Links relacionados ao estorno.</summary>
        public ICollection<SubscriptionLink> Links { get; set; } = [];
    }
}
