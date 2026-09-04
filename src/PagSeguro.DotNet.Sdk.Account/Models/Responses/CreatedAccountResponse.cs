using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Account.Models.Requests;

namespace PagSeguro.DotNet.Sdk.Account.Models.Responses
{
    /// <summary>
    /// Resultado da criação de uma conta PagBank, incluindo o token de acesso emitido.
    /// <see href="https://developer.pagbank.com.br/reference/criar-conta">ler documentação</see>
    /// </summary>
    public class CreatedAccountResponse : AccountRequest
    {
        /// <summary>
        /// Identificador único da conta criada.
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// Data e horário em que a conta foi criada.
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Token de autenticação emitido para a conta recém-criada.
        /// </summary>
        public TokenResponse? Token { get; set; }
    }
}
