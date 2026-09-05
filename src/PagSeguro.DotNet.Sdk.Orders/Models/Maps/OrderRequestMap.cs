using AutoMapper;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Maps
{
    public class OrderRequestMap : Profile
    {
        public OrderRequestMap()
        {
            // Sem isto o AutoMapper transforma coleções nulas em coleções vazias, e a API
            // recusa qr_codes: [] / items: [] com "must have at least 1 element".
            AllowNullCollections = true;
            CreateMap(typeof(OrderRequest), typeof(ChargedOrderRequest<>));
        }
    }
}
