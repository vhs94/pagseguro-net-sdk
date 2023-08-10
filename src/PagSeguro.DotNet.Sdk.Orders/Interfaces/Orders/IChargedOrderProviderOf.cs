using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.ChargedOrder;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders
{
    public interface IChargedOrderProviderOf<TChargeWriteDto, TChargeReadDto> : IProvider
        where TChargeWriteDto : ChargeDto
        where TChargeReadDto : ChargeDto
    {
        IChargedOrderProviderOf<TChargeWriteDto, TChargeReadDto> AddCharge(TChargeWriteDto chargeWriteDto);
        IChargedOrderProviderOf<TChargeWriteDto, TChargeReadDto> AddCharges(ICollection<TChargeWriteDto> chargeWriteDtos);
        IChargedOrderProviderOf<TChargeWriteDto, TChargeReadDto> Load(ChargedOrderWriteDto<TChargeWriteDto> chargedWriteDto);
        IChargedOrderProviderOf<TChargeWriteDto, TChargeReadDto> Load(OrderWriteDto orderWriteDto);
        ChargedOrderWriteDto<TChargeWriteDto> Build();
        Task<ChargedOrderReadDto<TChargeReadDto>> CreateAsync();
        Task<ChargedOrderReadDto<TChargeReadDto>> GetByIdAsync(string orderId);
        Task<ChargedOrderReadDto<TChargeReadDto>> PayAsync(string orderId);
    }
}
