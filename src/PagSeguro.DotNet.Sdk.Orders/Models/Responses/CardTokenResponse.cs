using PagSeguro.DotNet.Sdk.Orders.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Cartão validado e armazenado pelo PagBank. Use o <see cref="Id" /> no
    /// lugar dos dados do cartão nas cobranças seguintes.
    /// <see href="https://developer.pagbank.com.br/reference/validar-armanezar-cartao-pagbank">ler documentação</see>
    /// </summary>
    public class CardTokenResponse
    {
        /// <summary>Identificador do cartão armazenado. Por exemplo, CARD_123.</summary>
        public string? Id { get; set; }

        /// <summary>Bandeira identificada. Por exemplo, visa ou mastercard.</summary>
        public string? Brand { get; set; }

        /// <summary>Seis primeiros dígitos do cartão.</summary>
        [JsonPropertyName("first_digits")]
        public string? FirstDigits { get; set; }

        /// <summary>Quatro últimos dígitos do cartão.</summary>
        [JsonPropertyName("last_digits")]
        public string? LastDigits { get; set; }

        /// <summary>Mês de expiração.</summary>
        [JsonPropertyName("exp_month")]
        public string? ExpMonth { get; set; }

        /// <summary>Ano de expiração.</summary>
        [JsonPropertyName("exp_year")]
        public string? ExpYear { get; set; }

        /// <summary>Portador do cartão.</summary>
        public CardTokenHolder? Holder { get; set; }
    }
}
