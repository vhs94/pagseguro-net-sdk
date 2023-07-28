using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders
{
    public class ChargeOrderDto
    {
        public string OrderId { get; set; }
        public ICollection<ChargeWriteDto> Charges { get; set; }

        public ChargeOrderDto()
        {
            Charges = new List<ChargeWriteDto>();
        }
    }
}
