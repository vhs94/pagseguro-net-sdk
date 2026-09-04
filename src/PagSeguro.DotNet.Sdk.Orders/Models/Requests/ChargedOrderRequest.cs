using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Pedido com as cobranças que serão pagas junto da sua criação.
    /// <see href="https://developer.pagbank.com.br/reference/pagar-pedido">ler documentação</see>
    /// </summary>
    public class ChargedOrderRequest<TChargeRequest> : OrderRequest
        where TChargeRequest : ChargeBase
    {
        /// <summary>
        /// Cobranças a serem criadas e pagas junto com o pedido.
        /// </summary>
        public ICollection<TChargeRequest> Charges { get; set; }

        public ChargedOrderRequest() => Charges = [];
    }
}
