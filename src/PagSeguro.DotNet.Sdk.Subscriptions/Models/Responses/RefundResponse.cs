using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;

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

        /// <summary>Situação do estorno.</summary>
        public string? Status { get; set; }

        /// <summary>Data e horário de criação do estorno.</summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedDate { get; set; }

        /// <summary>Links relacionados ao estorno.</summary>
        public ICollection<SubscriptionLink> Links { get; set; } = [];
    }
}
