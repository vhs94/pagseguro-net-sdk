using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Account.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Account.Models.Requests
{
    /// <summary>
    /// Dados enviados para criar uma nova conta PagBank.
    /// <see href="https://developer.pagbank.com.br/reference/criar-conta">ler documentação</see>
    /// </summary>
    public class AccountRequest : AccountBase
    {
        /// <summary>
        /// Dados cadastrais da empresa. Obrigatório para contas SELLER e ENTERPRISE.
        /// </summary>
        public CompanyRequest? Company { get; set; }
        /// <summary>
        /// Informações do aceite dos termos de uso.
        /// </summary>
        [JsonPropertyName("tos_acceptance")]
        public TosAcceptanceRequest? TosAcceptance { get; set; }
    }
}
