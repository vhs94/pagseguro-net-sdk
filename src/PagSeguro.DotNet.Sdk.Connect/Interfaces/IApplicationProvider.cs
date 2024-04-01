using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Application;

namespace PagSeguro.DotNet.Sdk.Connect.Interfaces
{
    public interface IApplicationProvider : IProvider
    {
        /// <summary>
        /// Este endpoint permite que você crie um recurso de aplicação.
        /// Criar uma aplicação permite que você realize ações em nome dos usuários.
        /// <see href="https://dev.pagbank.uol.com.br/reference/criar-aplicacao">ler documentação</see>
        /// </summary>
        Task<ApplicationReadDto> CreateAsync(ApplicationWriteDto applicationWriteDto);

        /// <summary>
        /// Este endpoint permite que você consulte detalhes de uma aplicação a partir do clientId
        /// <see href="https://dev.pagbank.uol.com.br/reference/consultar-aplicacao">ler documentação</see>
        /// </summary>
        Task<ApplicationReadDto> GetByClientIdAsync(string clientId);
    }
}
