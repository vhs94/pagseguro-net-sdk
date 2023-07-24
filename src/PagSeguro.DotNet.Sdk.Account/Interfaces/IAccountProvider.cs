using PagSeguro.DotNet.Sdk.Account.Dtos;
using PagSeguro.DotNet.Sdk.Common.Interfaces;

namespace PagSeguro.DotNet.Sdk.Account.Interfaces
{
    public interface IAccountProvider : IProvider
    {
        Task<CreatedAccountDto> CreateAccountAsync(AccountWriteDto accountWriteDto);
        Task<AccountReadDto> GetAccountByIdAsync(string accountId);
    }
}
