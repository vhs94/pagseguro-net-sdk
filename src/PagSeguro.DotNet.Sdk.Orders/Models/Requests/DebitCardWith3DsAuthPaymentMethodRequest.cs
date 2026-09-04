using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Meio de pagamento com cartão de débito e autenticação 3DS.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-3ds-validacao-externa">ler documentação</see>
    /// </summary>
    public class DebitCardWith3DsAuthPaymentMethodRequest : DebitCardPaymentMethodRequest
    {
        /// <summary>
        /// Dados da autenticação 3DS.
        /// </summary>
        [JsonPropertyName("authentication_method")]
        public AuthenticationMethodRequest? AuthenticationMethod { get; set; }
    }
}
