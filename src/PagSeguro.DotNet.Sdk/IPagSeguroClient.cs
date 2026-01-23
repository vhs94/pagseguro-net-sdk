using PagSeguro.DotNet.Sdk.Account.Interfaces;
using PagSeguro.DotNet.Sdk.Certificate.Interfaces;
using PagSeguro.DotNet.Sdk.Connect.Interfaces;
using PagSeguro.DotNet.Sdk.Connect.Models.Requests;
using PagSeguro.DotNet.Sdk.Connect.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Fees;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;
using PagSeguro.DotNet.Sdk.PublicKey.Interfaces;

namespace PagSeguro.DotNet.Sdk
{
    public interface IPagSeguroClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IAuthorizationProvider ForAuthorization();

        IApplicationProvider ForApplication();

        /// <summary>
        /// Returns an account provider that can be used to access account-related operations.
        /// <see href="https://developer.pagbank.com.br/docs/cadastro-de-clientes">Read the docs</see>
        /// </summary>
        IAccountProvider ForAccount();

        IPublicKeyProvider ForPublicKey();

        IOrderProvider ForOrder();

        IChargeWithPaymentMethodProvider ForCharge();

        /// <summary>
        /// Returns a certificate provider that can be used to Create digital certificates
        /// <see href="https://developer.pagbank.com.br/docs/certificado-digital">Read the docs</see>
        /// </summary>
        IDigitalCertificateProvider ForCertificate();

        /// <summary>
        /// Returns a fee provider that can be used to calculate fees.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-taxas-transacao">Read the docs</see>
        /// </summary>
        IFeeProvider ForFee();

        Task<AuthorizationCodeResponse> ConnectAsync(AuthorizationCodeRequest authorizationCodeWriteDto);
        Task ConnectChallengeAsync();
        void ConfigureClientApplication(string clientId, string clientSecret);
    }
}
