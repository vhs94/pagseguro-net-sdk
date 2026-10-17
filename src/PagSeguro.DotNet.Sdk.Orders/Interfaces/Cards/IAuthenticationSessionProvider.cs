using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Cards
{
    /// <summary>
    /// Sessão de autenticação 3DS. A sessão é criada no back-end e repassada ao
    /// SDK de front-end, que conduz a autenticação do portador e devolve o
    /// <c>authentication_method.id</c> usado na cobrança.
    /// <see href="https://developer.pagbank.com.br/reference/criar-sessao-autenticacao-3ds">ler documentação</see>
    /// </summary>
    public interface IAuthenticationSessionProvider
    {
        /// <summary>
        /// Cria uma sessão de autenticação 3DS, válida por 30 minutos.
        /// Corresponde a POST /checkout-sdk/sessions.
        /// <see href="https://developer.pagbank.com.br/reference/criar-sessao-autenticacao-3ds">ler documentação</see>
        /// </summary>
        /// <returns>A sessão criada e o momento em que ela expira.</returns>
        Task<AuthenticationSessionResponse> CreateAsync();
    }
}
