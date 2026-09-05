using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Taxas agrupadas por meio de pagamento.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-taxas-transacao">ler documentação</see>
    /// </summary>
    public class PaymentMethodInfo
    {
        /// <summary>
        /// Taxas e planos de parcelamento do cartão de crédito.
        /// </summary>
        [JsonPropertyName("credit_card")]
        public CreditCardInfo? CreditCard { get; set; }
    }
}
