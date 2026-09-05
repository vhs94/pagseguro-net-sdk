namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Endereço de entrega do pedido.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-order">ler documentação</see>
    /// </summary>
    public class ShippingAddress : Address
    {
        /// <summary>
        /// Complemento do endereço. De 1 a 40 caracteres.
        /// </summary>
        public string? Complement { get; set; }
    }
}
