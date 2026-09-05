using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Splits
{
    /// <summary>
    /// Consulta e liberação da divisão de pagamento (split) de um pedido.
    /// Exige que a conta autenticada seja a recebedora principal.
    /// <see href="https://developer.pagbank.com.br/docs/config-split">ler documentação</see>
    /// </summary>
    public interface ISplitProvider
    {
        /// <summary>
        /// Consulta a divisão de pagamento pelo identificador.
        /// Corresponde a GET /splits/{split_id}.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-divisao-do-pagamento">ler documentação</see>
        /// </summary>
        /// <param name="splitId">Identificador da divisão. Por exemplo, SPLI_123.</param>
        /// <returns>A divisão, com o valor destinado a cada recebedor.</returns>
        Task<SplitResponse> GetByIdAsync(string splitId);

        /// <summary>
        /// Libera os valores que estavam retidos em custódia para os recebedores
        /// informados. Só a conta dona da transação pode liberar.
        /// Corresponde a POST /splits/{split_id}/custody/release.
        /// <see href="https://developer.pagbank.com.br/reference/liberar-divisao-de-pagamento-com-custodia">ler documentação</see>
        /// </summary>
        /// <param name="splitId">Identificador da divisão. Por exemplo, SPLI_123.</param>
        /// <param name="splitCustodyReleaseRequest">Recebedores que terão o valor liberado.</param>
        Task ReleaseCustodyAsync(string splitId, SplitCustodyReleaseRequest splitCustodyReleaseRequest);
    }
}
