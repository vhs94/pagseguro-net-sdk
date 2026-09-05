using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>
    /// Dados comuns de um cupom de desconto.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-cupom">ler documentação</see>
    /// </summary>
    public abstract class CouponBase
    {
        /// <summary>Identificador próprio atribuído ao cupom.</summary>
        [JsonPropertyName("reference_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ReferenceId { get; set; }

        /// <summary>Nome do cupom.</summary>
        public string? Name { get; set; }

        /// <summary>Descrição do cupom.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        /// <summary>Desconto concedido pelo cupom.</summary>
        public CouponDiscount? Discount { get; set; }

        /// <summary>Por quantos ciclos o cupom é aplicado. Obrigatório.</summary>
        public CouponDuration? Duration { get; set; }

        /// <summary>Quantidade máxima de resgates do cupom.</summary>
        [JsonPropertyName("redemption_limit")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? RedemptionLimit { get; set; }

        /// <summary>Data de expiração do cupom, no formato AAAA-MM-DD.</summary>
        [JsonPropertyName("exp_at")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ExpiresAt { get; set; }
    }
}
