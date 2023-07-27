using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip
{
    public class BankSlipDto
    {
        [JsonProperty("due_date")]
        public DateTime DueDate { get; set; }
        [JsonProperty("instruction_lines")]
        public InstructionLinesDto InstructionLines { get; set; }
        public BankSlipHolderDto Holder { get; set; }
    }
}