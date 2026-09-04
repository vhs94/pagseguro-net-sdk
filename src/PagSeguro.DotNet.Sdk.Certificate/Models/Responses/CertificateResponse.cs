namespace PagSeguro.DotNet.Sdk.Certificate.Models.Responses
{
    /// <summary>
    /// Certificado digital gerado para a conta, utilizado na comunicação autenticada com o PagBank.
    /// <see href="https://developer.pagbank.com.br/reference/criar-certificado-digital">ler documentação</see>
    /// </summary>
    public class CertificateResponse
    {
        /// <summary>
        /// Identificador do certificado.
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// Chave privada do certificado, codificada em Base64.
        /// </summary>
        public string? Key { get; set; }
        /// <summary>
        /// Certificado digital no formato PEM, codificado em Base64.
        /// </summary>
        public string? Pem { get; set; }
    }
}
