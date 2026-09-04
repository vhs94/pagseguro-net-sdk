using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Dados do cartão representado por um token de bandeira.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-token-bandeira">ler documentação</see>
    /// </summary>
    public class NetworkTokenCardRequest : NetworkTokenCardBase
    {
        /// <summary>
        /// Número do token de bandeira. De 14 a 19 caracteres.
        /// </summary>
        [JsonPropertyName("network_token")]
        public string? NetworkToken { get; set; }
        /// <summary>
        /// Código de segurança do cartão (CVV). De 3 a 4 caracteres.
        /// </summary>
        [JsonPropertyName("security_code")]
        public int SecurityCode { get; set; }
    }
}
