using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Resultado da autenticação 3DS retornado na cobrança.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-3ds-validacao-externa">ler documentação</see>
    /// </summary>
    public class AuthenticationMethodResponse : AuthenticationMethodBase
    {
        /// <summary>
        /// Status da autenticação 3DS.
        /// </summary>
        public string? Status { get; set; }
    }
}
