using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.ChargedOrder;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders
{
    public interface IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> : IProvider
        where TChargeWriteDto : ChargeDto
        where TChargeReadDto : ChargeDto
    {
        IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> AddCharge(TChargeWriteDto chargeWriteDto);
        IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> AddCharges(ICollection<TChargeWriteDto> chargeWriteDtos);
        IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> Load(ChargedOrderWriteDto<TChargeWriteDto> chargedWriteDto);
        IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> Load(OrderWriteDto orderWriteDto);
        ChargedOrderWriteDto<TChargeWriteDto> Build();
        Task<ChargedOrderReadDto<TChargeReadDto>> CreateAsync();
        Task<ChargedOrderReadDto<TChargeReadDto>> GetByIdAsync(string orderId);
        Task<ChargedOrderReadDto<TChargeReadDto>> PayAsync(string orderId);
    }
}
