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
    /// <inheritdoc cref="ICreditCardWith3DsAuthOrderProvider" />
    public class CreditCardWith3DsAuthOrderProvider(
        PagSeguroSettings settings,
        IMapper mapper,
        IFlurlClient flurlClient)
        : BaseProvider(settings, flurlClient),
        ICreditCardWith3DsAuthOrderProvider
    {
        private ChargedOrderRequest<ChargeByCreditCardWith3DsAuthRequest> _chargedOrderRequest = new();

        /// <inheritdoc />
        public ICreditCardWith3DsAuthOrderProvider AddCharge(ChargeByCreditCardWith3DsAuthRequest chargeRequest)
        {
            _chargedOrderRequest.Charges.Add(chargeRequest);
            return this;
        }

        /// <inheritdoc />
        public ICreditCardWith3DsAuthOrderProvider AddCharges(ICollection<ChargeByCreditCardWith3DsAuthRequest> chargeRequests)
        {
            List<ChargeByCreditCardWith3DsAuthRequest> newCharges = _chargedOrderRequest.Charges.ToList();
            newCharges.AddRange(chargeRequests);
            _chargedOrderRequest.Charges = newCharges;
            return this;
        }

        /// <inheritdoc />
        public ICreditCardWith3DsAuthOrderProvider Load(ChargedOrderRequest<ChargeByCreditCardWith3DsAuthRequest> chargedRequest)
        {
            _chargedOrderRequest = chargedRequest;
            return this;
        }

        /// <inheritdoc />
        public ICreditCardWith3DsAuthOrderProvider Load(OrderRequest orderRequest)
        {
            _chargedOrderRequest = mapper.Map<ChargedOrderRequest<ChargeByCreditCardWith3DsAuthRequest>>(orderRequest);
            return this;
        }

        /// <inheritdoc />
        public ChargedOrderRequest<ChargeByCreditCardWith3DsAuthRequest> Build()
        {
            ChargedOrderRequest<ChargeByCreditCardWith3DsAuthRequest> order = _chargedOrderRequest;
            _chargedOrderRequest = new ChargedOrderRequest<ChargeByCreditCardWith3DsAuthRequest>();
            return order;
        }

        /// <inheritdoc />
        public async Task<ChargedOrderResponse<ChargeByCreditCardWith3DsAuthResponse>> CreateAsync()
        {
            var orderResponse = await Request()
                .AppendPathSegment(OrderEndpoint.Orders)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey, Guid.NewGuid())
                .PostJsonAsync(_chargedOrderRequest)
                .ReceiveJson<ChargedOrderResponse<ChargeByCreditCardWith3DsAuthResponse>>();
            _chargedOrderRequest = new ChargedOrderRequest<ChargeByCreditCardWith3DsAuthRequest>();
            return orderResponse;
        }

        /// <inheritdoc />
        public async Task<ChargedOrderResponse<ChargeByCreditCardWith3DsAuthResponse>> GetByIdAsync(string orderId)
        {
            return await Request()
                .AppendPathSegment(OrderEndpoint.Orders)
                .AppendPathSegment(orderId)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<ChargedOrderResponse<ChargeByCreditCardWith3DsAuthResponse>>();
        }

        /// <inheritdoc />
        public async Task<ChargedOrderResponse<ChargeByCreditCardWith3DsAuthResponse>> PayAsync(string orderId)
        {
            var orderResponse = await Request()
                .AppendPathSegments(OrderEndpoint.Orders, orderId, OrderEndpoint.Pay)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(new
                {
                    charges = _chargedOrderRequest.Charges
                })
                .ReceiveJson<ChargedOrderResponse<ChargeByCreditCardWith3DsAuthResponse>>();
            _chargedOrderRequest = new ChargedOrderRequest<ChargeByCreditCardWith3DsAuthRequest>();
            return orderResponse;
        }
    }
}
