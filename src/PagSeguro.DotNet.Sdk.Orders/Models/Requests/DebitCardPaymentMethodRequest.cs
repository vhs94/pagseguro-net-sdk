using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Meio de pagamento com cartão de débito enviado na cobrança.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-cartao">ler documentação</see>
    /// </summary>
    public class DebitCardPaymentMethodRequest : DebitCardPaymentMethodBase
    {
        /// <summary>
        /// Dados do cartão de débito.
        /// </summary>
        public CardRequest? Card { get; set; }
    }
}
