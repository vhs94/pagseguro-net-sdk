using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Dados comuns de um item do pedido.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-order">ler documentação</see>
    /// </summary>
    public abstract class ItemBase
    {
        /// <summary>
        /// Identificador único atribuído para o item. De 1 a 255 caracteres.
        /// </summary>
        [JsonPropertyName("reference_id")]
        public string? ReferenceId { get; set; }
        /// <summary>
        /// Nome dado ao item. De 1 a 200 caracteres.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Quantidade referente ao item.
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Valor unitário do item, em centavos.
        /// </summary>
        [JsonPropertyName("unit_amount")]
        public int UnitAmount { get; set; }
    }
}
