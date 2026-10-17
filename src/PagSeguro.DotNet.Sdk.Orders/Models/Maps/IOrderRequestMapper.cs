using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Maps
{
    /// <summary>
    /// Converte um pedido já montado em um pedido com cobranças.
    /// </summary>
    public interface IOrderRequestMapper
    {
        /// <summary>
        /// Copia os dados de <paramref name="orderRequest"/> para um novo
        /// <see cref="ChargedOrderRequest{TChargeRequest}"/>, ainda sem cobranças.
        /// </summary>
        /// <typeparam name="TChargeRequest">Tipo da cobrança que será adicionada ao pedido.</typeparam>
        /// <param name="orderRequest">Pedido de origem.</param>
        ChargedOrderRequest<TChargeRequest> ToChargedOrder<TChargeRequest>(OrderRequest orderRequest)
            where TChargeRequest : ChargeBase;
    }
}
