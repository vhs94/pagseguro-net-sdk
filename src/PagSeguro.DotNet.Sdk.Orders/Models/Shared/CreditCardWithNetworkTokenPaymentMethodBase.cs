namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Dados comuns do pagamento com cartão de crédito usando token de bandeira.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-token-bandeira">ler documentação</see>
    /// </summary>
    public abstract class CreditCardWithNetworkTokenPaymentMethodBase : CardPaymentMethodBase
    {
        protected CreditCardWithNetworkTokenPaymentMethodBase()
            : base(PaymentMethodType.CreditCard)
        {
        }
    }
}
