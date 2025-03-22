using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Item;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.QrCode;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders
{
    public class OrderReadDto : OrderDto
    {
        public string? Id { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreateDate { get; set; }
        public ICollection<LinkDto> Links { get; set; }
        [JsonPropertyName("qr_codes")]
        public ICollection<QrCodeReadDto> QrCodes { get; set; }
        public ICollection<ItemReadDto> Items { get; set; }

        public OrderReadDto()
        {
            Links = [];
            QrCodes = [];
            Items = [];
        }
    }
}
