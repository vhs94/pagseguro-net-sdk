using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Meio de pagamento com cartão de crédito usando token de bandeira.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-token-bandeira">ler documentação</see>
    /// </summary>
    public class CreditCardWithNetworkTokenPaymentMethodRequest : CreditCardWithNetworkTokenPaymentMethodBase
    {
        /// <summary>
        /// Dados do cartão representado pelo token de bandeira.
        /// </summary>
        public NetworkTokenCardRequest? Card { get; set; }
    }
}
