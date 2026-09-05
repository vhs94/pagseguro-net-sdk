using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Interfaces
{
    /// <summary>
    /// Consulta das faturas geradas pelas assinaturas.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-fatura">ler documentação</see>
    /// </summary>
    public interface IInvoiceProvider : IProvider
    {
        /// <summary>
        /// Consulta uma fatura pelo identificador. Corresponde a GET /invoices/{invoice_id}.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-fatura">ler documentação</see>
        /// </summary>
        /// <param name="invoiceId">Identificador da fatura. Por exemplo, INVO_123.</param>
        /// <returns>A fatura encontrada.</returns>
        Task<InvoiceResponse> GetByIdAsync(string invoiceId);

        /// <summary>
        /// Lista os pagamentos de uma fatura.
        /// Corresponde a GET /invoices/{invoice_id}/payments.
        /// <see href="https://developer.pagbank.com.br/reference/listar-pagamento-da-fatura">ler documentação</see>
        /// </summary>
        /// <param name="invoiceId">Identificador da fatura.</param>
        /// <returns>Os pagamentos da fatura.</returns>
        Task<SubscriptionPaymentListResponse> ListPaymentsAsync(string invoiceId);
    }
}
