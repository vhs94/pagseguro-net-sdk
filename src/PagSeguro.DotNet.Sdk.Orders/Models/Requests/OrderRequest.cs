using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Dados enviados para criar um pedido.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pedido">ler documentação</see>
    /// </summary>
    public class OrderRequest : OrderBase
    {
        /// <summary>
        /// QR Codes Pix a serem gerados para o pedido.
        /// </summary>
        [JsonPropertyName("qr_codes")]
        public ICollection<QrCodeRequest> QrCodes { get; set; }
        /// <summary>
        /// Contém as informações dos itens inseridos no pedido.
        /// </summary>
        public ICollection<ItemRequest> Items { get; set; }

        public OrderRequest()
        {
            QrCodes = [];
            Items = [];
        }
    }
}
