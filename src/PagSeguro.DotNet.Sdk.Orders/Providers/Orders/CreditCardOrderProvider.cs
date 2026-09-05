using AutoMapper;
using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Orders
{
    /// <inheritdoc cref="ICreditCardOrderProvider" />
    public class CreditCardOrderProvider(
        PagSeguroSettings settings,
        IMapper mapper,
        IFlurlClient flurlClient)
        : BaseProvider(settings, flurlClient),
        ICreditCardOrderProvider
    {
        private ChargedOrderRequest<ChargeByCreditCardRequest> _chargedOrderRequest = new();

        /// <inheritdoc />
        public ICreditCardOrderProvider AddCharge(ChargeByCreditCardRequest chargeRequest)
        {
            _chargedOrderRequest.Charges.Add(chargeRequest);
            return this;
        }

        /// <inheritdoc />
        public ICreditCardOrderProvider AddCharges(ICollection<ChargeByCreditCardRequest> chargeRequests)
        {
            List<ChargeByCreditCardRequest> newCharges = _chargedOrderRequest.Charges.ToList();
            newCharges.AddRange(chargeRequests);
            _chargedOrderRequest.Charges = newCharges;
            return this;
        }

        /// <inheritdoc />
        public ICreditCardOrderProvider Load(ChargedOrderRequest<ChargeByCreditCardRequest> chargedRequest)
        {
            _chargedOrderRequest = chargedRequest;
            return this;
        }

        /// <inheritdoc />
        public ICreditCardOrderProvider Load(OrderRequest orderRequest)
        {
            _chargedOrderRequest = mapper.Map<ChargedOrderRequest<ChargeByCreditCardRequest>>(orderRequest);
            return this;
        }

        /// <inheritdoc />
        public ChargedOrderRequest<ChargeByCreditCardRequest> Build()
        {
            ChargedOrderRequest<ChargeByCreditCardRequest> order = _chargedOrderRequest;
            _chargedOrderRequest = new ChargedOrderRequest<ChargeByCreditCardRequest>();
            return order;
        }

        /// <inheritdoc />
        public async Task<ChargedOrderResponse<ChargeByCreditCardResponse>> CreateAsync()
        {
            var orderResponse = await Request()
                .AppendPathSegment(OrderEndpoint.Orders)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey, Guid.NewGuid())
                .PostJsonAsync(_chargedOrderRequest)
                .ReceiveJson<ChargedOrderResponse<ChargeByCreditCardResponse>>();
            _chargedOrderRequest = new ChargedOrderRequest<ChargeByCreditCardRequest>();
            return orderResponse;
        }

        /// <inheritdoc />
        public async Task<ChargedOrderResponse<ChargeByCreditCardResponse>> GetByIdAsync(string orderId)
        {
            return await Request()
                .AppendPathSegment(OrderEndpoint.Orders)
                .AppendPathSegment(orderId)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<ChargedOrderResponse<ChargeByCreditCardResponse>>();
        }

        /// <inheritdoc />
        public async Task<ChargedOrderResponse<ChargeByCreditCardResponse>> PayAsync(string orderId)
        {
            var orderResponse = await Request()
                .AppendPathSegments(OrderEndpoint.Orders, orderId, OrderEndpoint.Pay)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(new
                {
                    charges = _chargedOrderRequest.Charges
                })
                .ReceiveJson<ChargedOrderResponse<ChargeByCreditCardResponse>>();
            _chargedOrderRequest = new ChargedOrderRequest<ChargeByCreditCardRequest>();
            return orderResponse;
        }
    }
}
