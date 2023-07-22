using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.PublicKey.Dtos;

namespace PagSeguro.DotNet.Sdk.PublicKey.Interfaces
{
    public interface IPublicKeyProvider : IProvider
    {
        Task<PublicKeyReadDto> CreatePublicKeyAsync();
        Task<PublicKeyReadDto> UpdatePublicKeyAsync();
        Task<PublicKeyReadDto> GetPublicKeyAsync();
    }
}
