using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip
{
    public class BankSlipReadDto : BankSlipDto
    {
        public string? Id { get; set; }
        [JsonProperty("barcode")]
        public string? BarCode { get; set; }
        [JsonProperty("formatted_barcode")]
        public string? FormattedBarCode { get; set; }
    }
}
