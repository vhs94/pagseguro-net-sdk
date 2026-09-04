using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Cobrança paga com cartão de crédito.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-cartao">ler documentação</see>
    /// </summary>
    public class ChargeByCreditCardRequest : ChargeByCardRequest
    {
        /// <summary>
        /// Meio de pagamento com cartão de crédito.
        /// </summary>
        [JsonPropertyName("payment_method")]
        public CreditCardPaymentMethodRequest? PaymentMethod { get; set; }
    }
}
