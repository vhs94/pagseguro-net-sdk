using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Checkout.Models.Shared
{
    /// <summary>
    /// Item exibido na página de checkout.
    /// <see href="https://developer.pagbank.com.br/reference/criar-checkout">ler documentação</see>
    /// </summary>
    public class CheckoutItem
    {
        /// <summary>
        /// Identificador único atribuído para o item.
        /// </summary>
        [JsonPropertyName("reference_id")]
        public string? ReferenceId { get; set; }

        /// <summary>
        /// Nome dado ao item.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Descrição do item.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Quantidade referente ao item.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Valor unitário do item, em centavos.
        /// </summary>
        [JsonPropertyName("unit_amount")]
        public int UnitAmount { get; set; }

        /// <summary>
        /// URL da imagem do item exibida no checkout.
        /// </summary>
        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }
    }
}
