using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Cobrança paga com cartão de débito e autenticação 3DS retornada pela API.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-3ds-validacao-externa">ler documentação</see>
    /// </summary>
    public class ChargeByDebitCardWith3DsAuthResponse : ChargeByCardResponse
    {
        /// <summary>
        /// Meio de pagamento com cartão de débito e autenticação 3DS.
        /// </summary>
        [JsonPropertyName("payment_method")]
        public DebitCardWith3DsAuthPaymentMethodResponse? PaymentMethod { get; set; }
    }
}
