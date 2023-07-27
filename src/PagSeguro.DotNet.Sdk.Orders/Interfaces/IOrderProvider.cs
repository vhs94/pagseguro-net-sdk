using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces
{
    public interface IOrderProvider : IProvider
    {
        Task<OrderReadDto> CreateOrderAsync(OrderWriteDto orderWriteDto);
    }
}
