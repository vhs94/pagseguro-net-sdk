using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Item retornado na consulta do pedido.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-order">ler documentação</see>
    /// </summary>
    public class ItemResponse : ItemBase
    {
        /// <summary>
        /// Peso do item, em gramas.
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// Dimensões do item.
        /// </summary>
        public Dimension? Dimensions { get; set; }
    }
}
