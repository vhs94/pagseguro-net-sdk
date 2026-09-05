using PagSeguro.DotNet.Sdk.Orders.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Pedido retornado pela API.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-pedido">ler documentação</see>
    /// </summary>
    public class OrderResponse : OrderBase
    {
        /// <summary>
        /// Identificador do pedido PagBank. 41 caracteres.
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// Data e horário em que o pedido foi criado.
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Links relacionados ao pedido.
        /// </summary>
        public ICollection<Link> Links { get; set; }
        /// <summary>
        /// QR Codes Pix gerados para o pedido.
        /// </summary>
        [JsonPropertyName("qr_codes")]
        public ICollection<QrCodeResponse> QrCodes { get; set; }
        /// <summary>
        /// Itens inseridos no pedido.
        /// </summary>
        public ICollection<ItemResponse> Items { get; set; }

        public OrderResponse()
        {
            Links = [];
            QrCodes = [];
            Items = [];
        }
    }
}
