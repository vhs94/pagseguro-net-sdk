using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges
{
    public interface IChargeProviderOf<TTopLevelProvider, TChargeWriteDto, TChargeReadDto>
        : IChargeProvider<TChargeWriteDto, TChargeReadDto>
        where TChargeWriteDto : ChargeDto, IChargeWriteDto, new()
        where TChargeReadDto : ChargeDto
        where TTopLevelProvider : IChargeProvider<TChargeWriteDto, TChargeReadDto>
    {
        public TTopLevelProvider WithAmount(ChargeAmountWriteDto chargeAmountWriteDto)
        {
            ChargeWriteDto.Amount = chargeAmountWriteDto;
            return (TTopLevelProvider)this;
        }

        public TTopLevelProvider WithDescription(string description)
        {
            ChargeWriteDto.Description = description;
            return (TTopLevelProvider)this;
        }

        public TTopLevelProvider WithId(string chargeId)
        {
            ChargeWriteDto.Id = chargeId;
            return (TTopLevelProvider)this;
        }

        public TTopLevelProvider WithNotificationUrl(string notificationUrl)
        {
            ChargeWriteDto.NotificationUrls.Add(notificationUrl);
            return (TTopLevelProvider)this;
        }

        public TTopLevelProvider WithNotificationUrls(ICollection<string> notificationUrls)
        {
            List<string> newNotificationUrls = ChargeWriteDto.NotificationUrls.ToList();
            newNotificationUrls.AddRange(notificationUrls);
            ChargeWriteDto.NotificationUrls = newNotificationUrls;
            return (TTopLevelProvider)this;
        }

        public TTopLevelProvider WithReferenceId(string referenceId)
        {
            ChargeWriteDto.ReferenceId = referenceId;
            return (TTopLevelProvider)this;
        }

        public TTopLevelProvider Load(TChargeWriteDto chargeWriteDto)
        {
            ChargeWriteDto = chargeWriteDto;
            return (TTopLevelProvider)this;
        }

        public TChargeWriteDto Build()
        {
            TChargeWriteDto charge = ChargeWriteDto;
            InitCharge();
            return charge;
        }
    }
}
