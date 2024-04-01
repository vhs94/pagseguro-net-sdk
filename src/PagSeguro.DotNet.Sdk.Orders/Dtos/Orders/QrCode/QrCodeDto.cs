using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.QrCode
{
    public abstract class QrCodeDto
    {
        [JsonProperty("expiration_date")]
        public DateTime? ExpirationDate { get; set; }
        public AmountDto Amount { get; set; }
    }
}
