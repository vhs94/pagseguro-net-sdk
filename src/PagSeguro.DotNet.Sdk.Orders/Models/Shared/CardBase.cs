using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Dados comuns do cartão utilizado na cobrança.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-charge">ler documentação</see>
    /// </summary>
    public abstract class CardBase
    {
        /// <summary>
        /// Mês de expiração do cartão. De 1 a 2 caracteres.
        /// </summary>
        [JsonPropertyName("exp_month")]
        public int ExpMonth { get; set; }
        /// <summary>
        /// Ano de expiração do cartão. De 3 a 4 caracteres.
        /// </summary>
        [JsonPropertyName("exp_year")]
        public int ExpYear { get; set; }
        /// <summary>
        /// Dados do portador do cartão.
        /// </summary>
        public Holder? Holder { get; set; }
    }
}
