using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces;

namespace PagSeguro.DotNet.Sdk.Orders.Providers
{
    public class GenericChargeProvider<TChargeWriteDto, TChargeReadDto>
        : BaseProvider, IGenericChargeProvider<TChargeWriteDto, TChargeReadDto>
        where TChargeWriteDto : ChargeDto
        where TChargeReadDto : ChargeDto
    {
        public GenericChargeProvider(PagSeguroSettings settings)
            : base(settings)
        {
        }

        public async Task<TChargeReadDto> ChargeAsync(TChargeWriteDto chargeWriteDto)
        {
            return await BaseUrl
                .AppendPathSegments(OrderEndpoint.Charges)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey, Guid.NewGuid())
                .PostJsonAsync(chargeWriteDto)
                .ReceiveJson<TChargeReadDto>();
        }

        public async Task<TChargeReadDto> GetChargeByIdAsync(string chargeId)
        {
            return await BaseUrl
                .AppendPathSegments(OrderEndpoint.Charges, chargeId)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<TChargeReadDto>();
        }

        public async Task<TChargeReadDto> CancelChargeAsync(CancelChargeDto cancelChargeDto)
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

        public async Task<TChargeReadDto> CaptureChargeAsync(CaptureChargeDto captureChargeDto)
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
