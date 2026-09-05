using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses
{
    /// <summary>
    /// Assinante retornado pela API.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-assinante">ler documentação</see>
    /// </summary>
    public class CustomerResponse : CustomerBase
    {
        /// <summary>Identificador do assinante. Por exemplo, CUST_123.</summary>
        public string? Id { get; set; }

        /// <summary>Meios de pagamento cadastrados para o assinante.</summary>
        [JsonPropertyName("billing_info")]
        public ICollection<BillingInfo> BillingInfo { get; set; } = [];

        /// <summary>Data e horário de criação do assinante.</summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedDate { get; set; }

        /// <summary>Data e horário da última alteração do assinante.</summary>
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedDate { get; set; }

        /// <summary>Links relacionados ao assinante.</summary>
        public ICollection<SubscriptionLink> Links { get; set; } = [];
    }
}
