using AutoMapper;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Maps
{
    public class OrderRequestMap : Profile
    {
        public OrderRequestMap()
        {
            CreateMap(typeof(OrderRequest), typeof(ChargedOrderRequest<>));
        }
    }
}
