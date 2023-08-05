using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Charges
{
    public abstract class GenericChargeProvider<TChargeWriteDto, TChargeReadDto>
        : BaseProvider, IGenericChargeProvider<TChargeWriteDto, TChargeReadDto>
        where TChargeWriteDto : ChargeDto, new()
        where TChargeReadDto : ChargeDto
    {
        protected TChargeWriteDto ChargeWriteDto { get; private set; }

        public GenericChargeProvider(PagSeguroSettings settings)
            : base(settings)
        {
            InitCharge();
        }

        private void InitCharge()
        {
            ChargeWriteDto = new TChargeWriteDto();
        }

        public IGenericChargeProvider<TChargeWriteDto, TChargeReadDto> WithDescription(string description)
        {
            ChargeWriteDto.Description = description;
            return this;
        }

        public IGenericChargeProvider<TChargeWriteDto, TChargeReadDto> WithNotificationUrl(string notificationUrl)
        {
            ChargeWriteDto.NotificationUrls.Add(notificationUrl);
            return this;
        }

        public IGenericChargeProvider<TChargeWriteDto, TChargeReadDto> WithNotificationUrls(
            ICollection<string> notificationUrls)
        {
            List<string> newNotificationUrls = ChargeWriteDto.NotificationUrls.ToList();
            newNotificationUrls.AddRange(notificationUrls);
            ChargeWriteDto.NotificationUrls = newNotificationUrls;
            return this;
        }

        public IGenericChargeProvider<TChargeWriteDto, TChargeReadDto> WithReferenceId(string referenceId)
        {
            ChargeWriteDto.ReferenceId = referenceId;
            return this;
        }

        public TChargeWriteDto Build()
        {
            TChargeWriteDto charge = ChargeWriteDto;
            InitCharge();
            return charge;
        }

        public async Task<TChargeReadDto> ChargeAsync()
        {
            TChargeReadDto chargeReadDto = await BaseUrl
                .AppendPathSegments(OrderEndpoint.Charges)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey, Guid.NewGuid())
                .PostJsonAsync(ChargeWriteDto)
                .ReceiveJson<TChargeReadDto>();
            InitCharge();
            return chargeReadDto;
        }

        public async Task<TChargeReadDto> GetByIdAsync(string chargeId)
        {
            return await BaseUrl
                .AppendPathSegments(OrderEndpoint.Charges, chargeId)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<TChargeReadDto>();
        }

        public async Task<TChargeReadDto> CancelAsync(CancelChargeDto cancelChargeDto)
        {
            return await BaseUrl
                .AppendPathSegments(OrderEndpoint.Charges, cancelChargeDto.ChargeId, OrderEndpoint.Cancel)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(new
                {
                    amount = new
                    {
                        value = cancelChargeDto.AmountValue
                    }
                })
                .ReceiveJson<TChargeReadDto>();
        }

        public async Task<TChargeReadDto> CaptureAsync(CaptureChargeDto captureChargeDto)
        {
            return await BaseUrl
                .AppendPathSegments(OrderEndpoint.Charges, captureChargeDto.ChargeId, OrderEndpoint.Capture)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(new
                {
                    amount = new
                    {
                        value = captureChargeDto.AmountValue
                    }
                })
                .ReceiveJson<TChargeReadDto>();
        }
    }
}
