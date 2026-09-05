using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>
    /// Política de retentativa de cobrança aplicada às faturas que não foram
    /// pagas. Vale para todas as assinaturas do vendedor.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-retentativa">ler documentação</see>
    /// </summary>
    public abstract class RetryPreferenceBase
    {
        /// <summary>
        /// Dias de espera até a primeira retentativa, contados a partir do
        /// vencimento da fatura. Valores aceitos: 1, 3, 5 e 7.
        /// </summary>
        [JsonPropertyName("first_try")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? FirstTry { get; set; }

        /// <summary>
        /// Dias de espera até a segunda retentativa, contados a partir da
        /// primeira. Valores aceitos: 1, 3, 5 e 7.
        /// </summary>
        [JsonPropertyName("second_try")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? SecondTry { get; set; }

        /// <summary>
        /// Dias de espera até a terceira retentativa, contados a partir da
        /// segunda. Valores aceitos: 1, 3, 5 e 7.
        /// </summary>
        [JsonPropertyName("third_try")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? ThirdTry { get; set; }

        /// <summary>
        /// O que fazer com a assinatura depois da terceira retentativa sem
        /// sucesso. Valores aceitos: SUSPEND e CANCEL.
        /// </summary>
        [JsonPropertyName("finally")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Finally { get; set; }
    }
}
