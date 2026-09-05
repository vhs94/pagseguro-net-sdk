using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Dados comuns do cartão representado por um token de bandeira.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-token-bandeira">ler documentação</see>
    /// </summary>
    public abstract class NetworkTokenCardBase : CardBase
    {
        /// <summary>
        /// Dados da tokenização gerados pela bandeira ou pela carteira digital.
        /// </summary>
        [JsonPropertyName("token_data")]
        public TokenData? TokenData { get; set; }
    }
}
