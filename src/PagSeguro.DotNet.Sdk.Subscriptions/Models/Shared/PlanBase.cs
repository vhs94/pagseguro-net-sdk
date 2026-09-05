using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>
    /// Dados comuns de um plano de assinatura.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-plano">ler documentação</see>
    /// </summary>
    public abstract class PlanBase
    {
        /// <summary>Identificador próprio atribuído ao plano. Máximo de 65 caracteres.</summary>
        [JsonPropertyName("reference_id")]
        public string? ReferenceId { get; set; }

        /// <summary>Nome do plano. Máximo de 65 caracteres.</summary>
        public string? Name { get; set; }

        /// <summary>Descrição do plano.</summary>
        public string? Description { get; set; }

        /// <summary>Valor cobrado a cada ciclo do plano.</summary>
        public Money? Amount { get; set; }

        /// <summary>Periodicidade de cobrança do plano.</summary>
        public PlanInterval? Interval { get; set; }

        /// <summary>Taxa de adesão cobrada na criação da assinatura, em centavos.</summary>
        /// <remarks>
        /// Nulo por padrão e omitido do JSON: a API recusa setup_fee: 0 com
        /// "must contain only digits greater than 0".
        /// </remarks>
        [JsonPropertyName("setup_fee")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? SetupFee { get; set; }

        /// <summary>Período de teste do plano.</summary>
        public PlanTrial? Trial { get; set; }

        /// <summary>Quantidade máxima de assinaturas permitidas no plano.</summary>
        [JsonPropertyName("limit_subscriptions")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? LimitSubscriptions { get; set; }

        /// <summary>
        /// Meios de pagamento aceitos pelo plano. Por exemplo, CREDIT_CARD e BOLETO.
        /// Planos diários não aceitam boleto.
        /// </summary>
        [JsonPropertyName("payment_method")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<string>? PaymentMethod { get; set; }
    }
}
