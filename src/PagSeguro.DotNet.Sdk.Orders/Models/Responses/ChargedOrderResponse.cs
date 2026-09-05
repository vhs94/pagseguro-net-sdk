using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Pedido retornado com as cobranças criadas e pagas.
    /// <see href="https://developer.pagbank.com.br/reference/pagar-pedido">ler documentação</see>
    /// </summary>
    public class ChargedOrderResponse<TChargeResponse> : OrderResponse
        where TChargeResponse : ChargeBase
    {
        /// <summary>
        /// Cobranças criadas junto com o pedido.
        /// </summary>
        public ICollection<TChargeResponse> Charges { get; set; }

        public ChargedOrderResponse() => Charges = [];
    }
}
