using Flurl.Http;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard;
using PagSeguro.DotNet.Sdk.Orders.Helpers;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods
{
    public interface ICreditCardChargeProviderOf<TTopLevelProvider, TChargeWriteDto, TChargeReadDto>
        : ICardChargeProviderOf<TTopLevelProvider, TChargeWriteDto, TChargeReadDto>
        where TChargeWriteDto : ChargeByCardWriteDto, IChargeWriteDto, new()
        where TChargeReadDto : ChargeByCardReadDto
        where TTopLevelProvider : IChargeProvider<TChargeWriteDto, TChargeReadDto>
    {
        public async Task<TChargeReadDto> CaptureAsync(int amountValue)
        {
            return await BaseUrl
                .AppendPathSegments(OrderEndpoint.Charges, ChargeWriteDto.Id, OrderEndpoint.Capture)
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
