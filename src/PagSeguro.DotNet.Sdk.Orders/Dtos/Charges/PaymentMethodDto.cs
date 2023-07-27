using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges
{
    public class PaymentMethodDto
    {
        public string Type { get; set; }
        public int Installments { get; set; }
        public bool Capture { get; set; }
        [JsonProperty("soft_descriptor")]
        public string SoftDescriptor { get; set; }
        public CardDto Card { get; set; }
        [JsonProperty("token_data")]
        public TokenDataDto TokenData { get; set; }
        [JsonProperty("authentication_method")]
        public AuthenticationMethodDto AuthenticationMethod { get; set; }
        public BankSlipDto Boleto { get; set; }
    }
}