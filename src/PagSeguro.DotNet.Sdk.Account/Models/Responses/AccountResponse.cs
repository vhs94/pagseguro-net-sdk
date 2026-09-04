using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Account.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Account.Models.Responses
{
    /// <summary>
    /// Dados de uma conta PagBank retornados na consulta.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-conta">ler documentação</see>
    /// </summary>
    public class AccountResponse : AccountBase
    {
        /// <summary>
        /// Identificador único da conta, no formato ACCO_XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX.
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// Data e horário em que a conta foi criada.
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Situação atual da conta.
        /// </summary>
        public string? Status { get; set; }
        /// <summary>
        /// Dados cadastrais da empresa vinculada à conta.
        /// </summary>
        public CompanyResponse? Company { get; set; }
    }
}
