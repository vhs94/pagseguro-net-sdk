using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Cobrança paga com cartão de crédito e autenticação 3DS retornada pela API.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-3ds-validacao-externa">ler documentação</see>
    /// </summary>
    public class ChargeByCreditCardWith3DsAuthResponse : ChargeByCardResponse
    {
        /// <summary>
        /// Meio de pagamento com cartão de crédito e autenticação 3DS.
        /// </summary>
        [JsonPropertyName("payment_method")]
        public CreditCardWith3DsAuthPaymentMethodResponse? PaymentMethod { get; set; }
    }
}
