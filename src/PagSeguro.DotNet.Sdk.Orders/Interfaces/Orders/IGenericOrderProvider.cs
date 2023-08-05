using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.ChargedOrder;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Item;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.QrCode;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Shipping;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders
{
    public interface IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> : IProvider
        where TChargeWriteDto : ChargeDto
        where TChargeReadDto : ChargeDto
    {
        IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> AddCharge(TChargeWriteDto chargeWriteDto);
        IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> AddCharges(ICollection<TChargeWriteDto> chargeWriteDtos);
        IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> WithCustomer(CustomerDto customerDto);
        IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> WithItem(ItemWriteDto itemWriteDto);
        IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> WithItems(ICollection<ItemWriteDto> itemWriteDtos);
        IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> WithNotificationUrl(string notificationUrl);
        IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> WithNotificationUrls(ICollection<string> notificationUrls);
        IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> WithQrCode(QrCodeWriteDto qrCodeWriteDto);
        IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> WithQrCodes(ICollection<QrCodeWriteDto> qrCodeWriteDtos);
        IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> WithReferenceId(string referenceId);
        IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> WithShipping(ShippingDto shippingDto);
        ChargedOrderWriteDto<TChargeWriteDto> Build();
        Task<ChargedOrderReadDto<TChargeReadDto>> CreateAsync();
        Task<ChargedOrderReadDto<TChargeReadDto>> GetByIdAsync(string orderId);
        Task<ChargedOrderReadDto<TChargeReadDto>> PayAsync(string orderId);
    }
}
