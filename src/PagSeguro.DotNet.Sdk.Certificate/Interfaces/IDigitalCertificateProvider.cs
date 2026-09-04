using PagSeguro.DotNet.Sdk.Certificate.Models.Responses;
using PagSeguro.DotNet.Sdk.Common.Interfaces;

namespace PagSeguro.DotNet.Sdk.Certificate.Interfaces
{
    /// <summary>
    /// Emissão do certificado digital utilizado na comunicação autenticada com o PagBank.
    /// <see href="https://developer.pagbank.com.br/reference/criar-certificado-digital">ler documentação</see>
    /// </summary>
    public interface IDigitalCertificateProvider : IProvider
    {
        /// <summary>
        /// Gera um certificado digital para a conta autorizada.
        /// Requer um access_token obtido pelo fluxo de desafio (challenge).
        /// Corresponde a POST /certificates.
        /// <see href="https://developer.pagbank.com.br/reference/criar-certificado-digital">ler documentação</see>
        /// </summary>
        Task<CertificateResponse> CreateAsync();
    }
}
