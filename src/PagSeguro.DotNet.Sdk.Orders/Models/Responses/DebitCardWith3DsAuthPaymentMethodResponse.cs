using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Meio de pagamento com cartão de débito e autenticação 3DS retornado na cobrança.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-3ds-validacao-externa">ler documentação</see>
    /// </summary>
    public class DebitCardWith3DsAuthPaymentMethodResponse : DebitCardPaymentMethodResponse
    {
        /// <summary>
        /// Resultado da autenticação 3DS.
        /// </summary>
        [JsonPropertyName("authentication_method")]
        public AuthenticationMethodResponse? AuthenticationMethod { get; set; }
    }
}
