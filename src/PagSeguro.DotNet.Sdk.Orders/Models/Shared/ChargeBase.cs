using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Dados comuns de uma cobrança, compartilhados entre a criação e a consulta.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-charge">ler documentação</see>
    /// </summary>
    public abstract class ChargeBase
    {
        /// <summary>
        /// Identificador da cobrança usado internamente pelo builder para
        /// direcionar as operações de captura e cancelamento.
        /// </summary>
        internal string? Id { get; set; }
        /// <summary>
        /// Identificador único atribuído para a cobrança. De 1 a 64 caracteres.
        /// </summary>
        [JsonPropertyName("reference_id")]
        public string? ReferenceId { get; set; }
        /// <summary>
        /// Descrição da cobrança. De 1 a 64 caracteres.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// URLs de webhook notificadas a cada alteração de status da cobrança.
        /// </summary>
        [JsonPropertyName("notification_urls")]
        public ICollection<string> NotificationUrls { get; set; }

        public ChargeBase() => NotificationUrls = [];
    }
}
