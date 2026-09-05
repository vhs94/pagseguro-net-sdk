using PagSeguro.DotNet.Sdk.Orders.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Boleto gerado para a cobrança.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-boleto">ler documentação</see>
    /// </summary>
    public class BankSlipResponse : BankSlipBase
    {
        /// <summary>
        /// Identificador do boleto.
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// Código de barras do boleto.
        /// </summary>
        [JsonPropertyName("barcode")]
        public string? BarCode { get; set; }
        /// <summary>
        /// Linha digitável do boleto, já formatada.
        /// </summary>
        [JsonPropertyName("formatted_barcode")]
        public string? FormattedBarCode { get; set; }
    }
}
