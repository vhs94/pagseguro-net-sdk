using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Models.Requests
{
    /// <summary>
    /// Informações do aceite dos termos de uso pelo titular da conta.
    /// <see href="https://developer.pagbank.com.br/reference/criar-conta">ler documentação</see>
    /// </summary>
    public class TosAcceptanceRequest
    {
        /// <summary>
        /// IP que identifica o dispositivo no qual o usuário concordou com os termos.
        /// </summary>
        [JsonPropertyName("user_ip")]
        public string? UserIp { get; set; }
        /// <summary>
        /// Momento em que o usuário concordou com os termos.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
