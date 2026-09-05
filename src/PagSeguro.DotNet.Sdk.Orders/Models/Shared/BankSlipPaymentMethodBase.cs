namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Dados comuns do pagamento com boleto.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-boleto">ler documentação</see>
    /// </summary>
    public abstract class BankSlipPaymentMethodBase : PaymentMethodBase
    {
        public BankSlipPaymentMethodBase()
            : base(PaymentMethodType.BankSlip)
        {
        }
    }
}
