using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces
{
    public interface IGenericChargeProvider<TChargeWriteDto, TChargeReadDto> : IProvider
    {
        Task<TChargeReadDto> ChargeAsync(TChargeWriteDto chargeWriteDto);
        Task<TChargeReadDto> GetChargeByIdAsync(string chargeId);
        Task<TChargeReadDto> CaptureChargeAsync(CaptureChargeDto captureChargeDto);
        Task<TChargeReadDto> CancelChargeAsync(CancelChargeDto cancelChargeDto);
    }
}
