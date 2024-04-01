using AutoMapper;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.ChargedOrder;

namespace PagSeguro.DotNet.Sdk.Orders.DtoMaps
{
    public class OrderWriteDtoMap : Profile
    {
        public OrderWriteDtoMap()
        {
            CreateMap(typeof(OrderWriteDto), typeof(ChargedOrderWriteDto<>));
        }
    }
}
