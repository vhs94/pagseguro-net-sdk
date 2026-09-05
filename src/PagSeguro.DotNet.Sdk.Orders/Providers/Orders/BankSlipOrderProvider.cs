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
    /// <inheritdoc cref="IBankSlipOrderProvider" />
    public class BankSlipOrderProvider(
        PagSeguroSettings settings,
        IMapper mapper,
        IFlurlClient flurlClient)
        : BaseProvider(settings, flurlClient),
        IBankSlipOrderProvider
    {
        private ChargedOrderRequest<ChargeByBankSlipRequest> _chargedOrderRequest = new();

        /// <inheritdoc />
        public IBankSlipOrderProvider AddCharge(ChargeByBankSlipRequest chargeRequest)
        {
            _chargedOrderRequest.Charges.Add(chargeRequest);
            return this;
        }

        /// <inheritdoc />
        public IBankSlipOrderProvider AddCharges(ICollection<ChargeByBankSlipRequest> chargeRequests)
        {
            List<ChargeByBankSlipRequest> newCharges = _chargedOrderRequest.Charges.ToList();
            newCharges.AddRange(chargeRequests);
            _chargedOrderRequest.Charges = newCharges;
            return this;
        }

        /// <inheritdoc />
        public IBankSlipOrderProvider Load(ChargedOrderRequest<ChargeByBankSlipRequest> chargedRequest)
        {
            _chargedOrderRequest = chargedRequest;
            return this;
        }

        /// <inheritdoc />
        public IBankSlipOrderProvider Load(OrderRequest orderRequest)
        {
            _chargedOrderRequest = mapper.Map<ChargedOrderRequest<ChargeByBankSlipRequest>>(orderRequest);
            return this;
        }

        /// <inheritdoc />
        public ChargedOrderRequest<ChargeByBankSlipRequest> Build()
        {
            ChargedOrderRequest<ChargeByBankSlipRequest> order = _chargedOrderRequest;
            _chargedOrderRequest = new ChargedOrderRequest<ChargeByBankSlipRequest>();
            return order;
        }

        /// <inheritdoc />
        public async Task<ChargedOrderResponse<ChargeByBankSlipResponse>> CreateAsync()
        {
            var orderResponse = await Request()
                .AppendPathSegment(OrderEndpoint.Orders)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey, Guid.NewGuid())
                .PostJsonAsync(_chargedOrderRequest)
                .ReceiveJson<ChargedOrderResponse<ChargeByBankSlipResponse>>();
            _chargedOrderRequest = new ChargedOrderRequest<ChargeByBankSlipRequest>();
            return orderResponse;
        }

        /// <inheritdoc />
        public async Task<ChargedOrderResponse<ChargeByBankSlipResponse>> GetByIdAsync(string orderId)
        {
            return await Request()
                .AppendPathSegment(OrderEndpoint.Orders)
                .AppendPathSegment(orderId)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<ChargedOrderResponse<ChargeByBankSlipResponse>>();
        }

        /// <inheritdoc />
        public async Task<ChargedOrderResponse<ChargeByBankSlipResponse>> PayAsync(string orderId)
        {
            var orderResponse = await Request()
                .AppendPathSegments(OrderEndpoint.Orders, orderId, OrderEndpoint.Pay)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(new
                {
                    charges = _chargedOrderRequest.Charges
                })
                .ReceiveJson<ChargedOrderResponse<ChargeByBankSlipResponse>>();
            _chargedOrderRequest = new ChargedOrderRequest<ChargeByBankSlipRequest>();
            return orderResponse;
        }
    }
}
