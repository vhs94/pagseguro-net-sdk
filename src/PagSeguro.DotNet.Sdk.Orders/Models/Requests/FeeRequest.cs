using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Parâmetros da simulação de taxas e parcelamento.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-taxas-transacao">ler documentação</see>
    /// </summary>
    public class FeeRequest
    {
        /// <summary>
        /// Meios de pagamento considerados na simulação.
        /// O SDK simula apenas cartão de crédito (CREDIT_CARD).
        /// </summary>
        public string PaymentMethods => PaymentMethodType.CreditCard.ToDescription();
        /// <summary>
        /// Valor original da transação, em centavos.
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// Quantidade máxima de parcelas permitidas.
        /// </summary>
        public int MaxInstallments { get; set; }
        /// <summary>
        /// Quantidade de parcelas sem juros custeadas pelo vendedor.
        /// </summary>
        public int MaxInstallmentsNoInterest { get; set; }
        /// <summary>
        /// Seis primeiros dígitos do cartão (BIN).
        /// </summary>
        public int CreditCardBin { get; set; }
    }
}
