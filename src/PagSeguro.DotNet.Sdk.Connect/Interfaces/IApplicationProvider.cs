using PagSeguro.DotNet.Sdk.Connect.Models.Requests;
using PagSeguro.DotNet.Sdk.Connect.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Connect.Interfaces
{
    /// <summary>
    /// Criação e consulta das aplicações Connect usadas para agir em nome dos usuários.
    /// <see href="https://developer.pagbank.com.br/reference/criar-aplicacao">ler documentação</see>
    /// </summary>
    public interface IApplicationProvider
    {
        /// <summary>
        /// Este endpoint permite que você crie um recurso de aplicação.
        /// Criar uma aplicação permite que você realize ações em nome dos usuários.
        /// <see href="https://developer.pagbank.com.br/reference/criar-aplicacao">ler documentação</see>
        /// </summary>
        /// <param name="applicationRequest">Dados da aplicação a ser criada.</param>
        /// <returns>A aplicação criada, com o clientId e o clientSecret emitidos.</returns>
        Task<ApplicationResponse> CreateAsync(ApplicationRequest applicationRequest);

        /// <summary>
        /// Este endpoint permite que você consulte detalhes de uma aplicação a partir do clientId
        /// <see href="https://developer.pagbank.com.br/reference/consultar-aplicacao">ler documentação</see>
        /// </summary>
        /// <param name="clientId">Identificador público da aplicação.</param>
        /// <returns>A aplicação encontrada.</returns>
        Task<ApplicationResponse> GetByClientIdAsync(string clientId);
    }
}
