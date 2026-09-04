namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Dados comuns do pagamento com cartão de débito.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-cartao">ler documentação</see>
    /// </summary>
    public abstract class DebitCardPaymentMethodBase : PaymentMethodBase
    {
        protected DebitCardPaymentMethodBase()
            : base(PaymentMethodType.DebitCard)
        {
        }
    }
}
