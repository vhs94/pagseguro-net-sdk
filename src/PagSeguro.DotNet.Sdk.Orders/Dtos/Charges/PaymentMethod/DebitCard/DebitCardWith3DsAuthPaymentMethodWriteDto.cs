using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.AuthenticationMethod;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.DebitCard
{
    public class DebitCardWith3DsAuthPaymentMethodWriteDto : DebitCardPaymentMethodWriteDto
    {
        [JsonProperty("authentication_method")]
        public AuthenticationMethodWriteDto? AuthenticationMethod { get; set; }
    }
}
