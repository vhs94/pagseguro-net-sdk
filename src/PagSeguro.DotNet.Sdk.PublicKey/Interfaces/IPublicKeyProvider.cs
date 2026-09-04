using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.PublicKey.Models.Responses;

namespace PagSeguro.DotNet.Sdk.PublicKey.Interfaces
{
    /// <summary>
    /// Gerenciamento das chaves públicas usadas para criptografar dados sensíveis,
    /// como os dados do cartão.
    /// <see href="https://developer.pagbank.com.br/reference/criar-chave-publica">ler documentação</see>
    /// </summary>
    public interface IPublicKeyProvider : IProvider
    {
        /// <summary>
        /// Cria uma chave pública para a conta.
        /// As chaves públicas são usadas na criptografia dos dados de cartão e na autenticação 3DS.
        /// Corresponde a POST /public-keys.
        /// <see href="https://developer.pagbank.com.br/reference/criar-chave-publica">ler documentação</see>
        /// </summary>
        /// <returns>A chave pública criada.</returns>
        Task<PublicKeyResponse> CreateAsync();
        /// <summary>
        /// Solicita a rotação da chave pública vinculada à conta.
        /// A chave pública anterior permanece válida por 7 dias após a alteração.
        /// Corresponde a PUT /public-keys/card.
        /// <see href="https://developer.pagbank.com.br/reference/alterar-chave-publica">ler documentação</see>
        /// </summary>
        /// <returns>A nova chave pública.</returns>
        Task<PublicKeyResponse> UpdateAsync();
        /// <summary>
        /// Consulta a chave pública vigente da conta.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-chave-publica">ler documentação</see>
        /// </summary>
        /// <returns>A chave pública vigente.</returns>
        Task<PublicKeyResponse> GetAsync();
    }
}
