using PagSeguro.DotNet.Sdk.Certificate.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Certificate.Interfaces
{
    public interface IDigitalCertificateProvider
    {
        /// <summary>
        /// This endpoint allows you to generate a digital certificate
        /// <see href="http://developer.pagbank.com.br/reference/criar-certificado-digital">Read the docs</see>
        /// </summary>
        /// <returns>An <see cref="CertificateResponse"/> containing the certificate details.</returns>
        /// <remarks>
        /// <para><strong>Warning:</strong> Before calling this method, you must call <c>ConnectChallengeAsync()</c> on the PagSeguroClient.</para>
        /// </remarks>
        Task<CertificateResponse> CreateAsync();
    }
}
