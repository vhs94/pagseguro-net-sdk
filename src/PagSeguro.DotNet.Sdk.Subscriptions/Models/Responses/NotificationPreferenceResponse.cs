using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses
{
    /// <summary>
    /// Preferências de notificação da conta.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-preferencias-notificacoes">ler documentação</see>
    /// </summary>
    public class NotificationPreferenceResponse
    {
        /// <summary>URLs de webhook notificadas pelos eventos de assinatura.</summary>
        public ICollection<string> Urls { get; set; } = [];

        /// <summary>Preferências de envio de e-mail.</summary>
        public NotificationEmailPreference? Email { get; set; }
    }
}
