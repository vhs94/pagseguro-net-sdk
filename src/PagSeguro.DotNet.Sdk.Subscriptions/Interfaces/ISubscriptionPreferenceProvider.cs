using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Interfaces
{
    /// <summary>
    /// Preferências de notificação e chave pública das cobranças recorrentes.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-preferencias-notificacoes">ler documentação</see>
    /// </summary>
    public interface ISubscriptionPreferenceProvider : IProvider
    {
        /// <summary>
        /// Consulta as preferências de notificação.
        /// Corresponde a GET /preferences/notifications.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-preferencias-notificacoes">ler documentação</see>
        /// </summary>
        /// <returns>As preferências vigentes.</returns>
        Task<NotificationPreferenceResponse> GetNotificationPreferencesAsync();

        /// <summary>
        /// Altera as preferências de notificação.
        /// Corresponde a PUT /preferences/notifications.
        /// <see href="https://developer.pagbank.com.br/reference/alterar-preferencias-notificacoes">ler documentação</see>
        /// </summary>
        /// <param name="notificationPreferenceRequest">Novas preferências.</param>
        /// <returns>As preferências atualizadas.</returns>
        Task<NotificationPreferenceResponse> UpdateNotificationPreferencesAsync(
            NotificationPreferenceRequest notificationPreferenceRequest);

        /// <summary>
        /// Consulta a chave pública usada nas cobranças recorrentes.
        /// Corresponde a GET /public-keys.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-chave-publica-pagamento-recorrente">ler documentação</see>
        /// </summary>
        /// <returns>A chave pública vigente.</returns>
        Task<SubscriptionPublicKeyResponse> GetPublicKeyAsync();
    }
}
