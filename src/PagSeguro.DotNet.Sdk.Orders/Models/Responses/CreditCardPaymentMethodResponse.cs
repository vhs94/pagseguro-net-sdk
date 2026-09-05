using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Meio de pagamento com cartão de crédito retornado na cobrança.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-cartao">ler documentação</see>
    /// </summary>
    public class CreditCardPaymentMethodResponse : CreditCardPaymentMethodBase
    {
        /// <summary>
        /// Dados do cartão de crédito utilizado.
        /// </summary>
        public CardResponse? Card { get; set; }
    }
}
