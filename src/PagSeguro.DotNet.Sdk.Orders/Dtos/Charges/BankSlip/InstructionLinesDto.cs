using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip
{
    public class InstructionLinesDto
    {
        [JsonProperty("line_1")]
        public string? FirstLine { get; set; }
        [JsonProperty("line_2")]
        public string? SecondLine { get; set; }
    }
}