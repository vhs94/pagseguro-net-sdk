using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Common.Converters;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip
{
    public abstract class BankSlipDto
    {
        [JsonProperty("due_date")]
        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime DueDate { get; set; }
        [JsonProperty("instruction_lines")]
        public InstructionLinesDto InstructionLines { get; set; }
        public BankSlipHolderDto Holder { get; set; }
    }
}