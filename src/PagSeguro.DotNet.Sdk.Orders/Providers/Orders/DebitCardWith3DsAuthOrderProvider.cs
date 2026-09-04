using AutoMapper;
using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Orders
{
    /// <inheritdoc cref="IDebitCardWith3DsAuthOrderProvider" />
    public class DebitCardWith3DsAuthOrderProvider(
        PagSeguroSettings settings,
        IMapper mapper,
        IFlurlClient flurlClient)
        : BaseProvider(settings, flurlClient),
        IDebitCardWith3DsAuthOrderProvider
    {
        private ChargedOrderRequest<ChargeByDebitCardWith3DsAuthRequest> _chargedOrderRequest = new();

        /// <inheritdoc />
        public IDebitCardWith3DsAuthOrderProvider AddCharge(ChargeByDebitCardWith3DsAuthRequest chargeRequest)
        {
            _chargedOrderRequest.Charges.Add(chargeRequest);
            return this;
        }

        /// <inheritdoc />
        public IDebitCardWith3DsAuthOrderProvider AddCharges(ICollection<ChargeByDebitCardWith3DsAuthRequest> chargeRequests)
        {
            List<ChargeByDebitCardWith3DsAuthRequest> newCharges = _chargedOrderRequest.Charges.ToList();
            newCharges.AddRange(chargeRequests);
            _chargedOrderRequest.Charges = newCharges;
            return this;
        }

        /// <inheritdoc />
        public IDebitCardWith3DsAuthOrderProvider Load(ChargedOrderRequest<ChargeByDebitCardWith3DsAuthRequest> chargedRequest)
        {
            _chargedOrderRequest = chargedRequest;
            return this;
        }

        /// <inheritdoc />
        public IDebitCardWith3DsAuthOrderProvider Load(OrderRequest orderRequest)
        {
            _chargedOrderRequest = mapper.Map<ChargedOrderRequest<ChargeByDebitCardWith3DsAuthRequest>>(orderRequest);
            return this;
        }

        /// <inheritdoc />
        public ChargedOrderRequest<ChargeByDebitCardWith3DsAuthRequest> Build()
        {
            ChargedOrderRequest<ChargeByDebitCardWith3DsAuthRequest> order = _chargedOrderRequest;
            _chargedOrderRequest = new ChargedOrderRequest<ChargeByDebitCardWith3DsAuthRequest>();
            return order;
        }

        /// <inheritdoc />
        public async Task<ChargedOrderResponse<ChargeByDebitCardWith3DsAuthResponse>> CreateAsync()
        {
            var orderResponse = await Request()
                .AppendPathSegment(OrderEndpoint.Orders)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey, Guid.NewGuid())
                .PostJsonAsync(_chargedOrderRequest)
                .ReceiveJson<ChargedOrderResponse<ChargeByDebitCardWith3DsAuthResponse>>();
            _chargedOrderRequest = new ChargedOrderRequest<ChargeByDebitCardWith3DsAuthRequest>();
            return orderResponse;
        }

        /// <inheritdoc />
        public async Task<ChargedOrderResponse<ChargeByDebitCardWith3DsAuthResponse>> GetByIdAsync(string orderId)
        {
            return await Request()
                .AppendPathSegment(OrderEndpoint.Orders)
                .AppendPathSegment(orderId)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<ChargedOrderResponse<ChargeByDebitCardWith3DsAuthResponse>>();
        }

        /// <inheritdoc />
        public async Task<ChargedOrderResponse<ChargeByDebitCardWith3DsAuthResponse>> PayAsync(string orderId)
        {
            var orderResponse = await Request()
                .AppendPathSegments(OrderEndpoint.Orders, orderId, OrderEndpoint.Pay)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(new
                {
                    charges = _chargedOrderRequest.Charges
                })
                .ReceiveJson<ChargedOrderResponse<ChargeByDebitCardWith3DsAuthResponse>>();
            _chargedOrderRequest = new ChargedOrderRequest<ChargeByDebitCardWith3DsAuthRequest>();
            return orderResponse;
        }
    }
}
