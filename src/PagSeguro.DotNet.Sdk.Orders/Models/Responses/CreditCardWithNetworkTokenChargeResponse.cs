using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Cobrança paga com token de bandeira retornada pela API.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-token-bandeira">ler documentação</see>
    /// </summary>
    public class CreditCardWithNetworkTokenChargeResponse : ChargeByCardResponse
    {
        /// <summary>
        /// Meio de pagamento com token de bandeira.
        /// </summary>
        [JsonPropertyName("payment_method")]
        public CreditCardWithNetworkTokenPaymentMethodResponse? PaymentMethod { get; set; }
    }
}
