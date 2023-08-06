using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges
{
    public interface IGenericChargeProvider<TChargeWriteDto, TChargeReadDto> : IProvider
        where TChargeWriteDto : ChargeDto
        where TChargeReadDto : ChargeDto
    {
        IGenericChargeProvider<TChargeWriteDto, TChargeReadDto> WithDescription(string description);
        IGenericChargeProvider<TChargeWriteDto, TChargeReadDto> WithNotificationUrl(string notificationUrl);
        IGenericChargeProvider<TChargeWriteDto, TChargeReadDto> WithNotificationUrls(ICollection<string> notificationUrls);
        IGenericChargeProvider<TChargeWriteDto, TChargeReadDto> WithReferenceId(string referenceId);
        IGenericChargeProvider<TChargeWriteDto, TChargeReadDto> Load(TChargeWriteDto chargeWriteDto);
        TChargeWriteDto Build();
        Task<TChargeReadDto> ChargeAsync();
        Task<TChargeReadDto> GetByIdAsync(string chargeId);
        Task<TChargeReadDto> CaptureAsync(CaptureChargeDto captureChargeDto);
        Task<TChargeReadDto> CancelAsync(CancelChargeDto cancelChargeDto);
    }
}
