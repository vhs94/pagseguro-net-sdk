using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip
{
    public class InstructionLinesDto
    {
        [JsonPropertyName("line_1")]
        public string? FirstLine { get; set; }
        [JsonPropertyName("line_2")]
        public string? SecondLine { get; set; }
    }
}