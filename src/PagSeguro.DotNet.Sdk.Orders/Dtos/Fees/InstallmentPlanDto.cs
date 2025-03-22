using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Fees
{
    public class InstallmentPlanDto
    {
        public int Installments { get; set; }
        [JsonPropertyName("installment_value")]
        public int InstallmentValue { get; set; }
        [JsonPropertyName("interest_free")]
        public bool InterestFree { get; set; }
        public AmountDto? Amount { get; set; }
    }
}