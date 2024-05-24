using PagSeguro.DotNet.Sdk.Account.Interfaces;
using PagSeguro.DotNet.Sdk.Certificate.Interfaces;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.AuthorizationCode;
using PagSeguro.DotNet.Sdk.Connect.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Fees;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;
using PagSeguro.DotNet.Sdk.PublicKey.Interfaces;

namespace PagSeguro.DotNet.Sdk
{
    public interface IPagSeguroClient
    {
        IAuthorizationProvider ForAuthorization();
        IApplicationProvider ForApplication();
        IAccountProvider ForAccount();
        IPublicKeyProvider ForPublicKey();
        IOrderProvider ForOrder();
        IChargeWithPaymentMethodProvider ForCharge();
        IDigitalCertificateProvider ForCertificate();
        IFeeProvider ForFee();
        Task<AuthorizationCodeReadDto> ConnectAsync(AuthorizationCodeWriteDto authorizationCodeWriteDto);
        Task ConnectChallengeAsync();
        void ConfigureClientApplication(string clientId, string clientSecret);
    }
}
