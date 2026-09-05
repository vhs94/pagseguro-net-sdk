using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Meio de pagamento com token de bandeira retornado na cobrança.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-token-bandeira">ler documentação</see>
    /// </summary>
    public class CreditCardWithNetworkTokenPaymentMethodResponse : CreditCardWithNetworkTokenPaymentMethodBase
    {
        /// <summary>
        /// Dados do cartão tokenizado utilizado.
        /// </summary>
        public NetworkTokenCardResponse? Card { get; set; }
    }
}
