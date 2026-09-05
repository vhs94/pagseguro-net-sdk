using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Connect.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Connect.Models.Requests
{
    /// <summary>
    /// Dados enviados para trocar o código recebido por SMS por um access_token.
    /// <see href="https://developer.pagbank.com.br/reference/obter-access-token">ler documentação</see>
    /// </summary>
    public class SmsTokenRequest : AuthorizationRequestBase
    {
        internal override string GrantType => ApiGrants.Sms;

        /// <summary>
        /// Identificador da autorização devolvido ao solicitar o SMS.
        /// </summary>
        public string? AuthorizationId { get; set; }

        /// <summary>
        /// Código de seis dígitos que o vendedor recebeu por SMS. No sandbox,
        /// use 123456 para simular sucesso e 200200 para simular erro.
        /// </summary>
        public string? SmsCode { get; set; }
    }
}
