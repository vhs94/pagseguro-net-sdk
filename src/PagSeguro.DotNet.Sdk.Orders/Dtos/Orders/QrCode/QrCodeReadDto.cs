using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.QrCode
{
    public class QrCodeReadDto : QrCodeDto
    {
        public string? Id { get; set; }
        public string? Text { get; set; }
        public ICollection<LinkDto> Links { get; set; }

        public QrCodeReadDto() => Links = [];
    }
}
