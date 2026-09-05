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
        /// <remarks>
        /// Nulo por padrão e omitido do JSON quando vazio: a API recusa qr_codes: []
        /// com "must have at least 1 element", o que quebraria todo pedido pago por
        /// cartão ou boleto.
        /// </remarks>
        [JsonPropertyName("qr_codes")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<QrCodeRequest>? QrCodes { get; set; }
        /// <summary>
        /// Contém as informações dos itens inseridos no pedido.
        /// </summary>
        /// <remarks>
        /// Nulo por padrão e omitido do JSON quando vazio, pela mesma razão de
        /// <see cref="QrCodes"/>.
        /// </remarks>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<ItemRequest>? Items { get; set; }
    }
}
