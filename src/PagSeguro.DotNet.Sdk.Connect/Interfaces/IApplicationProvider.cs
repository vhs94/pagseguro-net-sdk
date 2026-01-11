using PagSeguro.DotNet.Sdk.Connect.Models.Requests;
using PagSeguro.DotNet.Sdk.Connect.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Connect.Interfaces
{
    public interface IApplicationProvider
    {
        /// <summary>
        /// This endpoint allows you to create an application resource.
        /// Creating an application allows you to perform actions on behalf of users.
        /// <see href="https://dev.pagbank.uol.com.br/reference/criar-aplicacao">Read the docs</see>
        /// </summary>
        Task<ApplicationResponse> CreateAsync(ApplicationRequest applicationRequest);

        /// <summary>
        /// This endpoint allows you to retrieve application details from the clientId
        /// <see href="https://dev.pagbank.uol.com.br/reference/consultar-aplicacao">Read the docs</see>
        /// </summary>
        Task<ApplicationResponse> GetByClientIdAsync(string clientId);
    }
}
