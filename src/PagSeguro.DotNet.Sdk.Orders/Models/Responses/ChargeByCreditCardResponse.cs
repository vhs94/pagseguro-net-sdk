using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Cobrança paga com cartão de crédito retornada pela API.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-cartao">ler documentação</see>
    /// </summary>
    public class ChargeByCreditCardResponse : ChargeByCardResponse
    {
        /// <summary>
        /// Meio de pagamento com cartão de crédito utilizado na cobrança.
        /// </summary>
        [JsonPropertyName("payment_method")]
        public CreditCardPaymentMethodResponse? PaymentMethod { get; set; }
    }
}
