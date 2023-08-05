using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.PublicKey.Dtos;

namespace PagSeguro.DotNet.Sdk.PublicKey.Interfaces
{
    public interface IPublicKeyProvider : IProvider
    {
        Task<PublicKeyReadDto> CreateAsync();
        Task<PublicKeyReadDto> UpdateAsync();
        Task<PublicKeyReadDto> GetAsync();
    }
}
