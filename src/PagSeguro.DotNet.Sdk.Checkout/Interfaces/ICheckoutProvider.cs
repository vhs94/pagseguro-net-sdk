using PagSeguro.DotNet.Sdk.Checkout.Models.Requests;
using PagSeguro.DotNet.Sdk.Checkout.Models.Responses;
using PagSeguro.DotNet.Sdk.Common.Interfaces;

namespace PagSeguro.DotNet.Sdk.Checkout.Interfaces
{
    /// <summary>
    /// Criação e gerenciamento do checkout, a página de pagamento hospedada pelo PagBank.
    /// <see href="https://developer.pagbank.com.br/reference/criar-checkout">ler documentação</see>
    /// </summary>
    public interface ICheckoutProvider : IProvider
    {
        /// <summary>
        /// Cria um checkout e devolve, entre os links, a relação PAY com o endereço
        /// da página de pagamento a ser aberta pelo comprador.
        /// Corresponde a POST /checkouts.
        /// <see href="https://developer.pagbank.com.br/reference/criar-checkout">ler documentação</see>
        /// </summary>
        /// <param name="checkoutRequest">Itens e configurações da página de pagamento.</param>
        /// <returns>O checkout criado, com os links SELF, PAY e INACTIVATE.</returns>
        Task<CheckoutResponse> CreateAsync(CheckoutRequest checkoutRequest);

        /// <summary>
        /// Consulta um checkout a partir do identificador fornecido pelo PagBank.
        /// Corresponde a GET /checkouts/{checkout_id}.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-checkout">ler documentação</see>
        /// </summary>
        /// <param name="checkoutId">Identificador do checkout. Por exemplo, CHEC_123.</param>
        /// <returns>O checkout encontrado.</returns>
        Task<CheckoutResponse> GetByIdAsync(string checkoutId);

        /// <summary>
        /// Inativa um checkout, impedindo novos pagamentos pela página.
        /// Corresponde a POST /checkouts/{checkout_id}/inactivate.
        /// <see href="https://developer.pagbank.com.br/reference/inativar-checkout">ler documentação</see>
        /// </summary>
        /// <param name="checkoutId">Identificador do checkout a ser inativado.</param>
        /// <returns>O checkout com a situação atualizada para INACTIVE.</returns>
        Task<CheckoutResponse> InactivateAsync(string checkoutId);

        /// <summary>
        /// Reativa um checkout previamente inativado.
        /// Corresponde a POST /checkouts/{checkout_id}/activate.
        /// <see href="https://developer.pagbank.com.br/reference/ativar-checkout">ler documentação</see>
        /// </summary>
        /// <param name="checkoutId">Identificador do checkout a ser reativado.</param>
        /// <returns>O checkout com a situação atualizada para ACTIVE.</returns>
        Task<CheckoutResponse> ActivateAsync(string checkoutId);
    }
}
