using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Fees
{
    public class CreditCardBrandDto
    {
        [JsonProperty("installment_plans")]
        public ICollection<InstallmentPlanDto> InstallmentPlans { get; set; }

        public CreditCardBrandDto()
        {
            InstallmentPlans = new List<InstallmentPlanDto>();
        }
    }
}