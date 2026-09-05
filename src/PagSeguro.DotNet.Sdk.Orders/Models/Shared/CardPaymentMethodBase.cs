namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Dados comuns dos meios de pagamento com cartão.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-charge">ler documentação</see>
    /// </summary>
    public abstract class CardPaymentMethodBase(PaymentMethodType type)
        : PaymentMethodBase(type)
    {
        /// <summary>
        /// Quantidade de parcelas. Obrigatório para cartão de crédito.
        /// </summary>
        public int Installments { get; set; }
        /// <summary>
        /// Define se a transação será capturada automaticamente (true)
        /// ou apenas pré-autorizada (false). Indisponível para cartão de débito.
        /// </summary>
        public bool Capture { get; set; }
    }
}
