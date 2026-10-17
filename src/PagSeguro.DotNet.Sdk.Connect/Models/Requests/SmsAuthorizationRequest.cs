using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Connect.Models.Requests
{
    /// <summary>
    /// Dados da conta bancária do vendedor que vai autorizar a aplicação por SMS.
    /// <see href="https://developer.pagbank.com.br/reference/solicitar-autorizacao-via-sms">ler documentação</see>
    /// </summary>
    public class SmsAuthorizationRequest
    {
        /// <summary>Agência da conta PagBank do vendedor.</summary>
        [JsonPropertyName("bank_branch")]
        public string? BankBranch { get; set; }

        /// <summary>
        /// Número da conta PagBank do vendedor, com o dígito verificador. Por
        /// exemplo, 12345678-9.
        /// </summary>
        [JsonPropertyName("account_number")]
        public string? AccountNumber { get; set; }
    }
}
