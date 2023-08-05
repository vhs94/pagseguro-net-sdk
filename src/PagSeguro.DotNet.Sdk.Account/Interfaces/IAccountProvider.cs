using PagSeguro.DotNet.Sdk.Account.Dtos;
using PagSeguro.DotNet.Sdk.Common.Interfaces;

namespace PagSeguro.DotNet.Sdk.Account.Interfaces
{
    public interface IAccountProvider : IProvider
    {
        Task<CreatedAccountDto> CreateAsync(AccountWriteDto accountWriteDto);
        Task<AccountReadDto> GetByIdAsync(string accountId);
    }
}
