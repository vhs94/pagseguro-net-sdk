using PagSeguro.DotNet.Sdk.Account.Models.Requests;
using PagSeguro.DotNet.Sdk.Account.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Account.Interfaces
{
    public interface IAccountProvider
    {

        /// <summary>
        /// Creates an account asynchronously using the PagBank API.
        /// <see href="https://developer.pagbank.com.br/reference/criar-conta">Read the docs</see>
        /// </summary>
        /// <param name="accountRequest">The account request payload.</param>
        /// <returns>A <see cref="CreatedAccountResponse"/> representing the created account.</returns>
        /// <remarks>
        /// <para><strong>Warning:</strong> Before calling this method, you must:</para>
        /// <list type="number">
        /// <item><description>Configure the ClientId and ClientSecret settings on the PagSeguroClient.</description></item>
        /// <item><description>Call <c>ConnectAsync()</c> on the PagSeguroClient to obtain an access token.</description></item>
        /// </list>
        /// </remarks>
        Task<CreatedAccountResponse> CreateAsync(AccountRequest accountRequest);

        /// <summary>
        /// Retrieves an account by its identifier asynchronously using the PagBank API.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-conta">Read the docs</see>
        /// </summary>
        /// <param name="accountId">The account identifier. e.g.: ACCO_123</param>
        /// <returns>An <see cref="AccountResponse"/> containing the account details.</returns>
        /// <remarks>
        /// <para><strong>Warning:</strong> Before calling this method, you must call <c>ConnectAsync()</c> on the PagSeguroClient to obtain an access token.</para>
        /// </remarks>
        Task<AccountResponse> GetByIdAsync(string accountId);
    }
}
