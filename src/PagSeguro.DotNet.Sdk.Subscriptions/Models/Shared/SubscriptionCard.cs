using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>
    /// Cartão usado na cobrança recorrente. No envio informe os dados do cartão
    /// (ou o campo encrypted); na resposta o PagBank devolve o token e os dados
    /// mascarados.
    /// </summary>
    public class SubscriptionCard
    {
        /// <summary>Dados do cartão criptografados com a chave pública.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Encrypted { get; set; }

        /// <summary>Número do cartão.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Number { get; set; }

        /// <summary>Código de segurança do cartão (CVV).</summary>
        [JsonPropertyName("security_code")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? SecurityCode { get; set; }

        /// <summary>Mês de expiração do cartão.</summary>
        [JsonPropertyName("exp_month")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ExpMonth { get; set; }

        /// <summary>Ano de expiração do cartão.</summary>
        [JsonPropertyName("exp_year")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ExpYear { get; set; }

        /// <summary>Portador do cartão.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CardHolder? Holder { get; set; }

        /// <summary>Token do cartão salvo, devolvido pelo PagBank.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Token { get; set; }

        /// <summary>Bandeira do cartão, devolvida pelo PagBank.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Brand { get; set; }

        /// <summary>Seis primeiros dígitos do cartão (BIN).</summary>
        [JsonPropertyName("first_digits")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FirstDigits { get; set; }

        /// <summary>Quatro últimos dígitos do cartão.</summary>
        [JsonPropertyName("last_digits")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LastDigits { get; set; }
    }
}
