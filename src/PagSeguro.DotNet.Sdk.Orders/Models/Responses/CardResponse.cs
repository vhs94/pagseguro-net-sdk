using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Dados do cartão retornados na cobrança.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-charge">ler documentação</see>
    /// </summary>
    public class CardResponse : CardBase
    {
        /// <summary>
        /// Bandeira do cartão. Até 20 caracteres.
        /// </summary>
        public string? Brand { get; set; }
        /// <summary>
        /// Seis primeiros números do cartão (BIN).
        /// </summary>
        [JsonPropertyName("first_digits")]
        public int FirstDigits { get; set; }
        /// <summary>
        /// Quatro últimos números do cartão.
        /// </summary>
        [JsonPropertyName("last_digits")]
        public int LastDigits { get; set; }
    }
}
