using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Cobrança paga com cartão de débito e autenticação 3DS.
    /// A autenticação 3DS é obrigatória para cartão de débito.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-3ds-validacao-externa">ler documentação</see>
    /// </summary>
    public class ChargeByDebitCardWith3DsAuthRequest : ChargeByCardRequest
    {
        /// <summary>
        /// Meio de pagamento com cartão de débito e autenticação 3DS.
        /// </summary>
        [JsonPropertyName("payment_method")]
        public DebitCardWith3DsAuthPaymentMethodRequest? PaymentMethod { get; set; }
    }
}
