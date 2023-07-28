using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces
{
    public interface IChargeProvider : IProvider
    {
        Task<OrderReadDto> ChargeOrderAsync(ChargeOrderDto chargeOrderDto);
        Task<ChargeReadDto> GetOrderChargeByIdAsync(string chargeId);
        Task<ChargeReadDto> CaptureChargeAsync(CaptureChargeDto captureChargeDto);
    }
}
