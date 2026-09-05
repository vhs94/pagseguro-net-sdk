using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses
{
    /// <summary>
    /// Cupom de desconto retornado pela API.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-cupom">ler documentação</see>
    /// </summary>
    public class CouponResponse : CouponBase
    {
        /// <summary>Identificador do cupom. Por exemplo, COUP_123.</summary>
        public string? Id { get; set; }

        /// <summary>Situação do cupom. Valores possíveis: ACTIVE e INACTIVE.</summary>
        public string? Status { get; set; }

        /// <summary>Indica se o cupom já está sendo usado por alguma assinatura.</summary>
        [JsonPropertyName("in_use")]
        public bool InUse { get; set; }

        /// <summary>Data e horário de criação do cupom.</summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedDate { get; set; }

        /// <summary>Data e horário da última alteração do cupom.</summary>
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedDate { get; set; }

        /// <summary>Links relacionados ao cupom.</summary>
        public ICollection<SubscriptionLink> Links { get; set; } = [];
    }
}
