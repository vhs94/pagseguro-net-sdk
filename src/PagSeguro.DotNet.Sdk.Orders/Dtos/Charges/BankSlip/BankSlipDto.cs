using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Common.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip
{
    public abstract class BankSlipDto
    {
        [JsonPropertyName("due_date")]
        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime DueDate { get; set; }
        [JsonPropertyName("instruction_lines")]
        public InstructionLinesDto? InstructionLines { get; set; }
        public BankSlipHolderDto? Holder { get; set; }
    }
}