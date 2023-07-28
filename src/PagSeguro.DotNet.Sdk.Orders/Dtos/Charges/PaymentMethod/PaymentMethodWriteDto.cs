using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges
{
    public class PaymentMethodWriteDto : PaymentMethodDto
    {
        [JsonProperty("soft_descriptor")]
        public string SoftDescriptor { get; set; }
        public CardWriteDto Card { get; set; }
        [JsonProperty("authentication_method")]
        public AuthenticationMethodDto AuthenticationMethod { get; set; }
        public BankSlipDto Boleto { get; set; }
    }
}
