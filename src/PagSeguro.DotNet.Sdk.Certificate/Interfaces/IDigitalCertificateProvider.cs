using PagSeguro.DotNet.Sdk.Certificate.Dtos;
using PagSeguro.DotNet.Sdk.Common.Interfaces;

namespace PagSeguro.DotNet.Sdk.Certificate.Interfaces
{
    public interface IDigitalCertificateProvider : IProvider
    {
        Task<CertificateReadDto> CreateAsync();
    }
}
