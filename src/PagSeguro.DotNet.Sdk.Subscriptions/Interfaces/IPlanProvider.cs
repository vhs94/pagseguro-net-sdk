using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Interfaces
{
    /// <summary>
    /// Gerenciamento dos planos de assinatura. Um plano define o valor e a
    /// periodicidade cobrados e é pré-requisito para criar assinaturas.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-plano">ler documentação</see>
    /// </summary>
    public interface IPlanProvider : IProvider
    {
        /// <summary>
        /// Cria um plano de assinatura.
        /// Corresponde a POST /plans.
        /// <see href="https://developer.pagbank.com.br/reference/criar-plano">ler documentação</see>
        /// </summary>
        /// <param name="planRequest">Dados do plano a ser criado.</param>
        /// <returns>O plano criado.</returns>
        Task<PlanResponse> CreateAsync(PlanRequest planRequest);

        /// <summary>
        /// Consulta um plano a partir do seu identificador.
        /// Corresponde a GET /plans/{plan_id}.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-por-id">ler documentação</see>
        /// </summary>
        /// <param name="planId">Identificador do plano. Por exemplo, PLAN_123.</param>
        /// <returns>O plano encontrado.</returns>
        Task<PlanResponse> GetByIdAsync(string planId);

        /// <summary>
        /// Lista os planos cadastrados.
        /// Corresponde a GET /plans.
        /// <see href="https://developer.pagbank.com.br/reference/listar-planos">ler documentação</see>
        /// </summary>
        /// <param name="offset">Deslocamento da página. Opcional.</param>
        /// <param name="limit">Quantidade máxima de registros por página. Opcional.</param>
        /// <returns>A página de planos, com as informações de paginação.</returns>
        Task<PlanListResponse> ListAsync(int? offset = null, int? limit = null);

        /// <summary>
        /// Altera um plano existente.
        /// Corresponde a PUT /plans/{plan_id}.
        /// <see href="https://developer.pagbank.com.br/reference/alterar-plano">ler documentação</see>
        /// </summary>
        /// <param name="planId">Identificador do plano a ser alterado.</param>
        /// <param name="planRequest">Novos dados do plano.</param>
        /// <returns>O plano alterado.</returns>
        Task<PlanResponse> UpdateAsync(string planId, PlanRequest planRequest);

        /// <summary>
        /// Ativa um plano previamente inativado.
        /// Corresponde a PUT /plans/{plan_id}/activate.
        /// <see href="https://developer.pagbank.com.br/reference/ativar-plano">ler documentação</see>
        /// </summary>
        /// <param name="planId">Identificador do plano a ser ativado.</param>
        Task ActivateAsync(string planId);

        /// <summary>
        /// Inativa um plano, impedindo novas assinaturas.
        /// Corresponde a PUT /plans/{plan_id}/inactivate.
        /// <see href="https://developer.pagbank.com.br/reference/inativar-plano">ler documentação</see>
        /// </summary>
        /// <param name="planId">Identificador do plano a ser inativado.</param>
        Task InactivateAsync(string planId);
    }
}
