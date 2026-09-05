using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Interfaces
{
    /// <summary>
    /// Gerenciamento dos cupons de desconto aplicáveis às assinaturas.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-cupom">ler documentação</see>
    /// </summary>
    public interface ICouponProvider : IProvider
    {
        /// <summary>
        /// Cria um cupom de desconto. Corresponde a POST /coupons.
        /// <see href="https://developer.pagbank.com.br/reference/criar-cupom">ler documentação</see>
        /// </summary>
        /// <param name="couponRequest">Desconto, duração e limites do cupom.</param>
        /// <returns>O cupom criado.</returns>
        Task<CouponResponse> CreateAsync(CouponRequest couponRequest);

        /// <summary>
        /// Consulta um cupom pelo identificador. Corresponde a GET /coupons/{coupon_id}.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-cupom">ler documentação</see>
        /// </summary>
        /// <param name="couponId">Identificador do cupom. Por exemplo, COUP_123.</param>
        /// <returns>O cupom encontrado.</returns>
        Task<CouponResponse> GetByIdAsync(string couponId);

        /// <summary>
        /// Lista os cupons cadastrados. Corresponde a GET /coupons.
        /// <see href="https://developer.pagbank.com.br/reference/listar-cupons">ler documentação</see>
        /// </summary>
        /// <param name="offset">Deslocamento da página. Opcional.</param>
        /// <param name="limit">Quantidade máxima de registros por página. Opcional.</param>
        /// <returns>A página de cupons.</returns>
        Task<CouponListResponse> ListAsync(int? offset = null, int? limit = null);

        /// <summary>
        /// Ativa um cupom. Corresponde a PUT /coupons/{coupon_id}/activate.
        /// <see href="https://developer.pagbank.com.br/reference/ativar-cupom">ler documentação</see>
        /// </summary>
        /// <param name="couponId">Identificador do cupom a ser ativado.</param>
        Task ActivateAsync(string couponId);

        /// <summary>
        /// Inativa um cupom. Corresponde a PUT /coupons/{coupon_id}/inactivate.
        /// <see href="https://developer.pagbank.com.br/reference/inativar-cupom">ler documentação</see>
        /// </summary>
        /// <param name="couponId">Identificador do cupom a ser inativado.</param>
        Task InactivateAsync(string couponId);
    }
}
