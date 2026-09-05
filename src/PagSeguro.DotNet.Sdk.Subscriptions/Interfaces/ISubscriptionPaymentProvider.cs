using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Interfaces
{
    /// <summary>
    /// Consulta dos pagamentos das faturas e criação de estornos.
    /// <see href="https://developer.pagbank.com.br/reference/listar-pagamentos">ler documentação</see>
    /// </summary>
    public interface ISubscriptionPaymentProvider : IProvider
    {
        /// <summary>
        /// Consulta um pagamento pelo identificador. Corresponde a GET /payments/{payment_id}.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-pagamento-1">ler documentação</see>
        /// </summary>
        /// <param name="paymentId">Identificador do pagamento. Por exemplo, PAYM_123.</param>
        /// <returns>O pagamento encontrado.</returns>
        Task<SubscriptionPaymentResponse> GetByIdAsync(string paymentId);

        /// <summary>
        /// Lista os pagamentos do vendedor. Corresponde a GET /payments.
        /// <see href="https://developer.pagbank.com.br/reference/listar-pagamentos">ler documentação</see>
        /// </summary>
        /// <param name="offset">Deslocamento da página. Opcional.</param>
        /// <param name="limit">Quantidade máxima de registros por página. Opcional.</param>
        /// <returns>A página de pagamentos.</returns>
        Task<SubscriptionPaymentListResponse> ListAsync(int? offset = null, int? limit = null);

        /// <summary>
        /// Estorna um pagamento, total ou parcialmente.
        /// Corresponde a POST /payments/{payment_id}/refunds.
        /// <see href="https://developer.pagbank.com.br/reference/criar-estorno-de-pagamento">ler documentação</see>
        /// </summary>
        /// <param name="paymentId">Identificador do pagamento a ser estornado.</param>
        /// <param name="refundRequest">Valor a ser estornado.</param>
        /// <returns>O estorno criado.</returns>
        Task<RefundResponse> RefundAsync(string paymentId, RefundRequest refundRequest);

        /// <summary>
        /// Lista os estornos de um pagamento.
        /// Corresponde a GET /payments/{payment_id}/refunds.
        /// <see href="https://developer.pagbank.com.br/reference/listar-estornos-do-payment">ler documentação</see>
        /// </summary>
        /// <param name="paymentId">Identificador do pagamento.</param>
        /// <returns>Os estornos do pagamento.</returns>
        Task<RefundListResponse> ListRefundsAsync(string paymentId);

        /// <summary>
        /// Lista todos os estornos do vendedor. Corresponde a GET /refunds.
        /// <see href="https://developer.pagbank.com.br/reference/listar-estornos-do-vendedor">ler documentação</see>
        /// </summary>
        /// <param name="offset">Deslocamento da página. Opcional.</param>
        /// <param name="limit">Quantidade máxima de registros por página. Opcional.</param>
        /// <returns>A página de estornos.</returns>
        Task<RefundListResponse> ListAllRefundsAsync(int? offset = null, int? limit = null);
    }
}
