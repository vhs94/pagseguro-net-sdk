using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests
{
    /// <summary>
    /// Preferências de notificação enviadas para alteração.
    /// <see href="https://developer.pagbank.com.br/reference/alterar-preferencias-notificacoes">ler documentação</see>
    /// </summary>
    public class NotificationPreferenceRequest
    {
        /// <summary>URLs de webhook notificadas pelos eventos de assinatura.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<string>? Urls { get; set; }

        /// <summary>Preferências de envio de e-mail.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public NotificationEmailPreference? Email { get; set; }
    }
}
