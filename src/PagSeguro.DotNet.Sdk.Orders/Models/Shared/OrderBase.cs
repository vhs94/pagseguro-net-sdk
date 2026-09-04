using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Dados comuns de um pedido, compartilhados entre a criação e a consulta.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-order">ler documentação</see>
    /// </summary>
    public abstract class OrderBase
    {
        /// <summary>
        /// Identificador único atribuído para o pedido. De 1 a 64 caracteres.
        /// </summary>
        [JsonPropertyName("reference_id")]
        public string? ReferenceId { get; set; }
        /// <summary>
        /// Contém as informações do cliente que está realizando o pedido.
        /// </summary>
        public Customer? Customer { get; set; }
        /// <summary>
        /// Contém as informações de entrega do pedido.
        /// </summary>
        public Shipping? Shipping { get; set; }
        /// <summary>
        /// URLs de webhook notificadas a cada alteração de status das cobranças.
        /// </summary>
        [JsonPropertyName("notification_urls")]
        public ICollection<string> NotificationUrls { get; set; }

        public OrderBase() => NotificationUrls = [];
    }
}
