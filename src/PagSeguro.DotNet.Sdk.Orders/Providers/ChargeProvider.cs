using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces;

namespace PagSeguro.DotNet.Sdk.Orders.Providers
{
    public class ChargeProvider : BaseProvider, IChargeProvider
    {
        public ChargeProvider(PagSeguroSettings settings)
            : base(settings)
        {
        }

        public async Task<OrderReadDto> ChargeOrderAsync(ChargeOrderDto chargeOrderDto)
        {
            return await BaseUrl
                .AppendPathSegments(OrderEndpoint.Orders, chargeOrderDto.OrderId, OrderEndpoint.Pay)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(new
                {
                    charges = chargeOrderDto.Charges
                })
                .ReceiveJson<OrderReadDto>();
        }

        public async Task<ChargeReadDto> GetOrderChargeByIdAsync(string chargeId)
        {
            return await BaseUrl
                .AppendPathSegments(OrderEndpoint.Charges, chargeId)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<ChargeReadDto>();
        }

        public async Task<ChargeReadDto> CaptureChargeAsync(CaptureChargeDto captureChargeDto)
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
                .ReceiveJson<ChargeReadDto>();
        }
    }
}
