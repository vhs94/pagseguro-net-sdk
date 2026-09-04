using PagSeguro.DotNet.Sdk.Common.Helpers;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Dados comuns do meio de pagamento utilizado na cobrança.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-charge">ler documentação</see>
    /// </summary>
    public abstract class PaymentMethodBase(PaymentMethodType type)
    {
        /// <summary>
        /// Tipo do meio de pagamento.
        /// Valores possíveis: CREDIT_CARD, DEBIT_CARD, BOLETO e PIX.
        /// </summary>
        public string Type { get; } = type.ToDescription();
    }
}
