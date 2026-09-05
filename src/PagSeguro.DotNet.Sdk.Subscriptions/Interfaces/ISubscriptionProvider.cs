using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Interfaces
{
    /// <summary>
    /// Gerenciamento das assinaturas, o vínculo entre um assinante e um plano.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-assinatura">ler documentação</see>
    /// </summary>
    public interface ISubscriptionProvider : IProvider
    {
        /// <summary>
        /// Cria uma assinatura.
        /// Corresponde a POST /subscriptions.
        /// <see href="https://developer.pagbank.com.br/reference/criar-assinatura">ler documentação</see>
        /// </summary>
        /// <param name="subscriptionRequest">Plano, assinante e meio de pagamento da assinatura.</param>
        /// <returns>A assinatura criada.</returns>
        Task<SubscriptionResponse> CreateAsync(SubscriptionRequest subscriptionRequest);

        /// <summary>
        /// Consulta uma assinatura a partir do seu identificador.
        /// Corresponde a GET /subscriptions/{subscription_id}.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-assinatura">ler documentação</see>
        /// </summary>
        /// <param name="subscriptionId">Identificador da assinatura. Por exemplo, SUBS_123.</param>
        /// <returns>A assinatura encontrada.</returns>
        Task<SubscriptionResponse> GetByIdAsync(string subscriptionId);

        /// <summary>
        /// Lista as assinaturas cadastradas.
        /// Corresponde a GET /subscriptions.
        /// <see href="https://developer.pagbank.com.br/reference/listar-assinaturas">ler documentação</see>
        /// </summary>
        /// <param name="offset">Deslocamento da página. Opcional.</param>
        /// <param name="limit">Quantidade máxima de registros por página. Opcional.</param>
        /// <returns>A página de assinaturas, com as informações de paginação.</returns>
        Task<SubscriptionListResponse> ListAsync(int? offset = null, int? limit = null);

        /// <summary>
        /// Lista as faturas de uma assinatura.
        /// Corresponde a GET /subscriptions/{subscription_id}/invoices.
        /// <see href="https://developer.pagbank.com.br/reference/listar-faturas-de-assinatura">ler documentação</see>
        /// </summary>
        /// <param name="subscriptionId">Identificador da assinatura.</param>
        /// <param name="offset">Deslocamento da página. Opcional.</param>
        /// <param name="limit">Quantidade máxima de registros por página. Opcional.</param>
        /// <returns>A página de faturas da assinatura.</returns>
        Task<InvoiceListResponse> ListInvoicesAsync(string subscriptionId, int? offset = null, int? limit = null);

        /// <summary>
        /// Altera uma assinatura.
        /// Corresponde a PUT /subscriptions/{subscription_id}.
        /// <see href="https://developer.pagbank.com.br/reference/alterar-assinatura">ler documentação</see>
        /// </summary>
        /// <param name="subscriptionId">Identificador da assinatura.</param>
        /// <param name="subscriptionUpdateRequest">Dados a serem alterados.</param>
        /// <returns>A assinatura alterada.</returns>
        Task<SubscriptionResponse> UpdateAsync(string subscriptionId, SubscriptionUpdateRequest subscriptionUpdateRequest);

        /// <summary>
        /// Suspende uma assinatura, interrompendo as próximas cobranças.
        /// Corresponde a PUT /subscriptions/{subscription_id}/suspend.
        /// <see href="https://developer.pagbank.com.br/reference/suspender-assinatura">ler documentação</see>
        /// </summary>
        /// <param name="subscriptionId">Identificador da assinatura a ser suspensa.</param>
        Task SuspendAsync(string subscriptionId);

        /// <summary>
        /// Reativa uma assinatura suspensa.
        /// Corresponde a PUT /subscriptions/{subscription_id}/activate.
        /// <see href="https://developer.pagbank.com.br/reference/ativar-assinatura">ler documentação</see>
        /// </summary>
        /// <param name="subscriptionId">Identificador da assinatura a ser reativada.</param>
        Task ActivateAsync(string subscriptionId);

        /// <summary>
        /// Cancela uma assinatura definitivamente.
        /// Corresponde a PUT /subscriptions/{subscription_id}/cancel.
        /// <see href="https://developer.pagbank.com.br/reference/cancelar-assinatura">ler documentação</see>
        /// </summary>
        /// <param name="subscriptionId">Identificador da assinatura a ser cancelada.</param>
        Task CancelAsync(string subscriptionId);

        /// <summary>
        /// Dispara manualmente uma nova tentativa de cobrança da assinatura.
        /// Corresponde a PUT /subscriptions/{subscription_id}/retry.
        /// <see href="https://developer.pagbank.com.br/reference/retentativa-de-cobranca-manual">ler documentação</see>
        /// </summary>
        /// <param name="subscriptionId">Identificador da assinatura a ser recobrada.</param>
        Task RetryAsync(string subscriptionId);
    }
}
