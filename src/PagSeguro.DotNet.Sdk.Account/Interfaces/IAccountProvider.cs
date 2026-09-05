using PagSeguro.DotNet.Sdk.Account.Models.Requests;
using PagSeguro.DotNet.Sdk.Account.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Account.Interfaces
{
    /// <summary>
    /// Operações de cadastro e consulta de contas PagBank.
    /// <see href="https://developer.pagbank.com.br/reference/criar-conta">ler documentação</see>
    /// </summary>
    public interface IAccountProvider
    {
        /// <summary>
        /// Cria uma nova conta PagBank.
        /// Corresponde a POST /accounts.
        /// <see href="https://developer.pagbank.com.br/reference/criar-conta">ler documentação</see>
        /// </summary>
        Task<CreatedAccountResponse> CreateAsync(AccountRequest accountRequest);
        /// <summary>
        /// Consulta os dados de uma conta a partir do seu identificador.
        /// Corresponde a GET /accounts/{account_id}.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-conta">ler documentação</see>
        /// </summary>
        Task<AccountResponse> GetByIdAsync(string accountId);
    }
}
