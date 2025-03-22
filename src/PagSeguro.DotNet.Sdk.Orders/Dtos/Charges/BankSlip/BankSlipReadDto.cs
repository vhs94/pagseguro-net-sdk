using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip
{
    public class BankSlipReadDto : BankSlipDto
    {
        public string? Id { get; set; }
        [JsonPropertyName("barcode")]
        public string? BarCode { get; set; }
        [JsonPropertyName("formatted_barcode")]
        public string? FormattedBarCode { get; set; }
    }
}
