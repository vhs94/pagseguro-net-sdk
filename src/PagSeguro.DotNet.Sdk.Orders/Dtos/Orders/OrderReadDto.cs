using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders
{
    public class OrderReadDto : OrderDto
    {
        public string Id { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreateDate { get; set; }
        public ICollection<LinkDto> Links { get; set; }
        [JsonProperty("qr_codes")]
        public ICollection<QrCodeReadDto> QrCodes { get; set; }
        public ICollection<ChargeDto> Charges { get; set; }

        public OrderReadDto()
        {
            Links = new List<LinkDto>();
            QrCodes = new List<QrCodeReadDto>();
            Charges = new List<ChargeDto>();
        }
    }
}
