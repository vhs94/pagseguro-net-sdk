using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Fees
{
    public class InstallmentPlanDto
    {
        public int Installments { get; set; }
        [JsonProperty("installment_value")]
        public int InstallmentValue { get; set; }
        [JsonProperty("interest_free")]
        public bool InterestFree { get; set; }
        public AmountDto Amount { get; set; }
    }
}