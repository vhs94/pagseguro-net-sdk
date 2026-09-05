using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Interfaces
{
    /// <summary>
    /// Gerenciamento dos assinantes das cobranças recorrentes.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-assinante">ler documentação</see>
    /// </summary>
    public interface ICustomerProvider
    {
        /// <summary>
        /// Cria um assinante.
        /// Corresponde a POST /customers.
        /// <see href="https://developer.pagbank.com.br/reference/criar-assinante">ler documentação</see>
        /// </summary>
        /// <param name="customerRequest">Dados do assinante, incluindo o meio de pagamento.</param>
        /// <returns>O assinante criado, com o cartão já tokenizado.</returns>
        Task<CustomerResponse> CreateAsync(CustomerRequest customerRequest);

        /// <summary>
        /// Consulta um assinante a partir do seu identificador.
        /// Corresponde a GET /customers/{customer_id}.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-assinante">ler documentação</see>
        /// </summary>
        /// <param name="customerId">Identificador do assinante. Por exemplo, CUST_123.</param>
        /// <returns>O assinante encontrado.</returns>
        Task<CustomerResponse> GetByIdAsync(string customerId);

        /// <summary>
        /// Lista os assinantes cadastrados.
        /// Corresponde a GET /customers.
        /// <see href="https://developer.pagbank.com.br/reference/listar-assinantes">ler documentação</see>
        /// </summary>
        /// <param name="offset">Deslocamento da página. Opcional.</param>
        /// <param name="limit">Quantidade máxima de registros por página. Opcional.</param>
        /// <returns>A página de assinantes, com as informações de paginação.</returns>
        Task<CustomerListResponse> ListAsync(int? offset = null, int? limit = null);

        /// <summary>
        /// Altera os dados cadastrais do assinante.
        /// Corresponde a PUT /customers/{customer_id}.
        /// <see href="https://developer.pagbank.com.br/reference/alterar-dados-cadastrais-do-assinante">ler documentação</see>
        /// </summary>
        /// <param name="customerId">Identificador do assinante.</param>
        /// <param name="customerUpdateRequest">Novos dados cadastrais.</param>
        /// <returns>O assinante alterado.</returns>
        /// <remarks>
        /// O documento (tax_id) não pode ser alterado e o meio de pagamento é
        /// alterado por <see cref="UpdateBillingInfoAsync"/>.
        /// </remarks>
        Task<CustomerResponse> UpdateAsync(string customerId, CustomerUpdateRequest customerUpdateRequest);

        /// <summary>
        /// Altera o meio de pagamento do assinante.
        /// Corresponde a PUT /customers/{customer_id}/billing_info.
        /// <see href="https://developer.pagbank.com.br/reference/alterar-dados-de-pagamento-do-assinante">ler documentação</see>
        /// </summary>
        /// <param name="customerId">Identificador do assinante.</param>
        /// <param name="billingInfoRequest">Novo meio de pagamento.</param>
        /// <returns>O assinante com o meio de pagamento atualizado.</returns>
        Task<CustomerResponse> UpdateBillingInfoAsync(string customerId, BillingInfoRequest billingInfoRequest);
    }
}
