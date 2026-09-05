using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses
{
    /// <summary>
    /// Plano de assinatura retornado pela API.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-plano">ler documentação</see>
    /// </summary>
    public class PlanResponse : PlanBase
    {
        /// <summary>Identificador do plano. Por exemplo, PLAN_123.</summary>
        public string? Id { get; set; }

        /// <summary>Situação do plano. Valores possíveis: ACTIVE e INACTIVE.</summary>
        public string? Status { get; set; }

        /// <summary>Data e horário de criação do plano.</summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedDate { get; set; }

        /// <summary>Data e horário da última alteração do plano.</summary>
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedDate { get; set; }

        /// <summary>Indica se o plano ainda pode ser alterado.</summary>
        public bool Editable { get; set; }

        /// <summary>Links relacionados ao plano.</summary>
        public ICollection<SubscriptionLink> Links { get; set; } = [];
    }
}
