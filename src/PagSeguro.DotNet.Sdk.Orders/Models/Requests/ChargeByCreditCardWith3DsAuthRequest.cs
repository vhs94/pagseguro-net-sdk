using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Cobrança paga com cartão de crédito e autenticação 3DS.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-3ds-validacao-externa">ler documentação</see>
    /// </summary>
    public class ChargeByCreditCardWith3DsAuthRequest : ChargeByCardRequest
    {
        /// <summary>
        /// Meio de pagamento com cartão de crédito e autenticação 3DS.
        /// </summary>
        [JsonPropertyName("payment_method")]
        public CreditCardWith3DsAuthPaymentMethodRequest? PaymentMethod { get; set; }
    }
}
