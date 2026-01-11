using PagSeguro.DotNet.Sdk.PublicKey.Models.Response;

namespace PagSeguro.DotNet.Sdk.PublicKey.Interfaces
{
    public interface IPublicKeyProvider
    {
        /// <summary>
        /// This endpoint allows you to create a public key. Public keys are used for card encryption and 3DS authentication.
        /// <see href="http://developer.pagbank.com.br/reference/criar-chave-publica">Read the docs</see>
        /// </summary>
        Task<PublicKeyResponse> CreateAsync();

        /// <summary>
        /// This endpoint allows you to change the public key linked to your account.
        /// The old public key will remain valid for 7 days after you perform the update.
        /// <see href="http://developer.pagbank.com.br/reference/alterar-chave-publica">Read the docs</see>
        /// </summary>
        Task<PublicKeyResponse> UpdateAsync();

        /// <summary>
        /// This endpoint allows you to retrieve the existing public key in your account.
        /// <see href="http://developer.pagbank.com.br/reference/consultar-chave-publica">Read the docs</see>
        /// </summary>
        Task<PublicKeyResponse> GetAsync();
    }
}
