using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Meio de pagamento com cartão de débito retornado na cobrança.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-cartao">ler documentação</see>
    /// </summary>
    public class DebitCardPaymentMethodResponse : DebitCardPaymentMethodBase
    {
        /// <summary>
        /// Dados do cartão de débito utilizado.
        /// </summary>
        public CardResponse? Card { get; set; }
    }
}
