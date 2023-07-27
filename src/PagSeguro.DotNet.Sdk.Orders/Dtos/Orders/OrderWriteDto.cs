using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders
{
    public class OrderWriteDto : OrderDto
    {
        [JsonProperty("qr_codes")]
        public ICollection<QrCodeWriteDto> QrCodes { get; set; }

        public OrderWriteDto()
        {
            QrCodes = new List<QrCodeWriteDto>();
        }
    }
}
