using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.QrCode
{
    public abstract class QrCodeDto
    {
        [JsonPropertyName("expiration_date")]
        public DateTime? ExpirationDate { get; set; }
        public AmountDto? Amount { get; set; }
    }
}
