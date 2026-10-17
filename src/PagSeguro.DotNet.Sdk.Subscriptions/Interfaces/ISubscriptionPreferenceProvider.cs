using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Interfaces
{
    /// <summary>
    /// Preferências de notificação e chave pública das cobranças recorrentes.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-preferencias-notificacoes">ler documentação</see>
    /// </summary>
    public interface ISubscriptionPreferenceProvider
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

        /// <summary>
        /// Gera uma nova chave pública para as cobranças recorrentes. A chave
        /// anterior deixa de valer, então recriptografe os cartões antes de
        /// enviá-los.
        /// Corresponde a PUT /public-keys.
        /// <see href="https://developer.pagbank.com.br/reference/criar-chave-publica-pagamento-recorrente">ler documentação</see>
        /// </summary>
        /// <returns>A chave pública recém-criada.</returns>
        Task<SubscriptionPublicKeyResponse> CreatePublicKeyAsync();

        /// <summary>
        /// Consulta a política de retentativa das faturas não pagas.
        /// Corresponde a GET /preferences/retries.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-retentativa">ler documentação</see>
        /// </summary>
        /// <returns>A política de retentativa vigente.</returns>
        Task<RetryPreferenceResponse> GetRetryPreferencesAsync();

        /// <summary>
        /// Altera a política de retentativa das faturas não pagas.
        /// Corresponde a PUT /preferences/retries.
        /// <see href="https://developer.pagbank.com.br/reference/alterar-retentativa">ler documentação</see>
        /// </summary>
        /// <param name="retryPreferenceRequest">Nova política de retentativa.</param>
        Task UpdateRetryPreferencesAsync(RetryPreferenceRequest retryPreferenceRequest);
    }
}
