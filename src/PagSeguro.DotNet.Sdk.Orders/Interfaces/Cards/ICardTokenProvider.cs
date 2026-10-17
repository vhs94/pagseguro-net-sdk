using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Cards
{
    /// <summary>
    /// Validação e armazenamento de cartões no PagBank. O token devolvido
    /// substitui os dados do cartão nas cobranças seguintes, evitando que eles
    /// trafeguem ou sejam guardados pela sua aplicação.
    /// <see href="https://developer.pagbank.com.br/reference/validar-armanezar-cartao-pagbank">ler documentação</see>
    /// </summary>
    public interface ICardTokenProvider
    {
        /// <summary>
        /// Valida o cartão e o armazena, devolvendo um token reutilizável.
        /// Corresponde a POST /tokens/cards.
        /// <see href="https://developer.pagbank.com.br/reference/validar-armanezar-cartao-pagbank">ler documentação</see>
        /// </summary>
        /// <param name="cardTokenRequest">
        /// Cartão criptografado, ou os dados abertos do cartão caso a integração
        /// seja certificada PCI.
        /// </param>
        /// <returns>O cartão armazenado, com o identificador a ser reutilizado.</returns>
        Task<CardTokenResponse> CreateAsync(CardTokenRequest cardTokenRequest);
    }
}
