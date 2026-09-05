using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Cobrança paga com cartão de crédito representado por um token de bandeira.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-token-bandeira">ler documentação</see>
    /// </summary>
    public class CreditCardWithNetworkTokenChargeRequest : ChargeByCardRequest
    {
        /// <summary>
        /// Meio de pagamento com token de bandeira.
        /// </summary>
        [JsonPropertyName("payment_method")]
        public CreditCardWithNetworkTokenPaymentMethodRequest? PaymentMethod { get; set; }
    }
}
