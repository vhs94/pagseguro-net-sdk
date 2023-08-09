using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Helpers;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges
{
    public interface IChargeProvider<TChargeWriteDto, TChargeReadDto> : IProvider
        where TChargeWriteDto : ChargeDto, IChargeWriteDto, new()
        where TChargeReadDto : ChargeDto
    {
        public TChargeWriteDto ChargeWriteDto { get; set; }

        internal void InitCharge()
        {
            ChargeWriteDto = new TChargeWriteDto();
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

        public async Task<TChargeReadDto> CancelAsync(int amountValue)
        {
            return await BaseUrl
                .AppendPathSegments(OrderEndpoint.Charges, ChargeWriteDto.Id, OrderEndpoint.Cancel)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(new
                {
                    amount = new
                    {
                        value = amountValue
                    }
                })
                .ReceiveJson<TChargeReadDto>();
        }
    }
}
