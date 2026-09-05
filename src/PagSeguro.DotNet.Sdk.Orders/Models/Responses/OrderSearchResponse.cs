namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Envelope devolvido pela busca de pedidos, que retorna a lista sob a chave orders.
    /// </summary>
    internal class OrderSearchResponse
    {
        /// <summary>
        /// Pedidos encontrados.
        /// </summary>
        public ICollection<OrderResponse> Orders { get; set; } = [];
    }
}
