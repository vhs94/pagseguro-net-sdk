using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders
{
    public class OrderWriteDto : OrderDto
    {
        [JsonProperty("qr_codes")]
        public ICollection<QrCodeWriteDto> QrCodes { get; set; }
        public ICollection<ChargeWriteDto> Charges { get; set; }
        public ICollection<ItemWriteDto> Items { get; set; }

        public OrderWriteDto()
        {
            QrCodes = new List<QrCodeWriteDto>();
            Charges = new List<ChargeWriteDto>();
            Items = new List<ItemWriteDto>();
        }
    }
}
