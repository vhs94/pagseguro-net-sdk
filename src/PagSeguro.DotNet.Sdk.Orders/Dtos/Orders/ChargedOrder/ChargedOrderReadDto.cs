using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.ChargedOrder
{
    public class ChargedOrderReadDto<TChargeReadDto> : OrderReadDto
        where TChargeReadDto : ChargeDto
    {
        public ICollection<TChargeReadDto> Charges { get; set; }

        public ChargedOrderReadDto()
        {
            Charges = new List<TChargeReadDto>();
        }
    }
}
