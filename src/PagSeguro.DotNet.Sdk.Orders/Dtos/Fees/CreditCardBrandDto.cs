using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Fees
{
    public class CreditCardBrandDto
    {
        [JsonPropertyName("installment_plans")]
        public ICollection<InstallmentPlanDto> InstallmentPlans { get; set; }

        public CreditCardBrandDto() => InstallmentPlans = [];
    }
}