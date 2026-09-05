using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Dados comuns de um QR Code Pix vinculado ao pedido.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pedido-com-qr-code-pix-v2">ler documentação</see>
    /// </summary>
    public abstract class QrCodeBase
    {
        /// <summary>
        /// Data de expiração do QR Code.
        /// Quando omitida, expira às 23:59 do dia seguinte.
        /// </summary>
        [JsonPropertyName("expiration_date")]
        public DateTime? ExpirationDate { get; set; }
        /// <summary>
        /// Valor do QR Code.
        /// </summary>
        public QrCodeAmount? Amount { get; set; }
    }
}
