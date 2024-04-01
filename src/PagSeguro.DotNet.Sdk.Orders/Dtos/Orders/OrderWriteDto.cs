using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Item;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.QrCode;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders
{
    public class OrderWriteDto : OrderDto
    {
        [JsonProperty("qr_codes")]
        public ICollection<QrCodeWriteDto> QrCodes { get; set; }
        public ICollection<ItemWriteDto> Items { get; set; }

        public OrderWriteDto()
        {
            QrCodes = new List<QrCodeWriteDto>();
            Items = new List<ItemWriteDto>();
        }
    }
}
