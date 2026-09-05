namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Informações de entrega do pedido.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-order">ler documentação</see>
    /// </summary>
    public class Shipping
    {
        /// <summary>
        /// Endereço de entrega do pedido.
        /// </summary>
        public ShippingAddress? Address { get; set; }
    }
}
