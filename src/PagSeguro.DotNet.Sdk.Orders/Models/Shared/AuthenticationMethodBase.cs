using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Dados comuns da autenticação 3DS aplicada à cobrança.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-3ds-validacao-externa">ler documentação</see>
    /// </summary>
    public abstract class AuthenticationMethodBase
    {
        /// <summary>
        /// Tipo da autenticação. Valores possíveis: THREEDS e INAPP.
        /// </summary>
        public string? Type { get; set; }
        /// <summary>
        /// Identificador único de autenticação. 80 caracteres.
        /// </summary>
        public string? Cavv { get; set; }
        /// <summary>
        /// Identificador MPI. 80 caracteres. Recomendado para a bandeira VISA.
        /// </summary>
        public string? Xid { get; set; }
        /// <summary>
        /// Indicador E-Commerce. 2 caracteres.
        /// </summary>
        public string? Eci { get; set; }
        /// <summary>
        /// Versão do protocolo 3DS utilizada. 10 caracteres.
        /// </summary>
        public string? Version { get; set; }
        /// <summary>
        /// Identificador da transação no diretório. 80 caracteres.
        /// Recomendado para a bandeira Mastercard.
        /// </summary>
        [JsonPropertyName("dstrans_id")]
        public string? DstransId { get; set; }
    }
}
