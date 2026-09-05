namespace PagSeguro.DotNet.Sdk.Connect.Interfaces
{
    /// <summary>
    /// Serviço de decriptação usado no fluxo de desafio do Connect.
    /// </summary>
    public interface ICryptoService
    {
        /// <summary>
        /// Decripta um conteúdo usando a chave privada configurada.
        /// </summary>
        /// <param name="encryptedContent">Conteúdo criptografado a ser decriptado.</param>
        /// <returns>O conteúdo decriptado.</returns>
        string Decrypt(string encryptedContent);
    }
}
