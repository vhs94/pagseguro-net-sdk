using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.ChargedOrder
{
    public class ChargedOrderWriteDto<TChargeWriteDto> : OrderWriteDto
        where TChargeWriteDto : ChargeDto
    {
        public ICollection<TChargeWriteDto> Charges { get; set; }

        public ChargedOrderWriteDto()
        {
            Charges = new List<TChargeWriteDto>();
        }
    }
}
