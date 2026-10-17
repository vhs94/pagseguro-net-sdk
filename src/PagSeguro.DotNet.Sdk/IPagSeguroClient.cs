using PagSeguro.DotNet.Sdk.Account.Interfaces;
using PagSeguro.DotNet.Sdk.Certificate.Interfaces;
using PagSeguro.DotNet.Sdk.Checkout.Interfaces;
using PagSeguro.DotNet.Sdk.Connect.Interfaces;
using PagSeguro.DotNet.Sdk.Connect.Models.Requests;
using PagSeguro.DotNet.Sdk.Connect.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Cards;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Fees;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Splits;
using PagSeguro.DotNet.Sdk.PublicKey.Interfaces;
using PagSeguro.DotNet.Sdk.Subscriptions.Interfaces;

namespace PagSeguro.DotNet.Sdk
{
    /// <summary>
    /// Ponto de entrada do SDK. Expõe os providers de cada serviço do PagBank
    /// e o fluxo de autenticação da integração.
    /// <see href="https://developer.pagbank.com.br/reference/introducao">ler documentação</see>
    /// </summary>
    public interface IPagSeguroClient : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Provider de emissão de access_token pelo Connect.
        /// </summary>
        IAuthorizationProvider ForAuthorization();
        /// <summary>
        /// Provider de criação e consulta de aplicações Connect.
        /// </summary>
        IApplicationProvider ForApplication();
        /// <summary>
        /// Provider de cadastro e consulta de contas PagBank.
        /// </summary>
        IAccountProvider ForAccount();
        /// <summary>
        /// Provider de gerenciamento das chaves públicas.
        /// </summary>
        IPublicKeyProvider ForPublicKey();
        /// <summary>
        /// Provider de criação e consulta de pedidos.
        /// </summary>
        IOrderProvider ForOrder();
        /// <summary>
        /// Provider de criação de cobranças avulsas.
        /// </summary>
        IChargeWithPaymentMethodProvider ForCharge();
        /// <summary>
        /// Provider de emissão do certificado digital.
        /// </summary>
        IDigitalCertificateProvider ForCertificate();
        /// <summary>
        /// Provider de simulação de taxas e parcelamento.
        /// </summary>
        IFeeProvider ForFee();

        /// <summary>
        /// Provider de criação e gerenciamento do checkout hospedado pelo PagBank.
        /// </summary>
        ICheckoutProvider ForCheckout();

        /// <summary>
        /// Provider de consulta e liberação da divisão de pagamento (split).
        /// </summary>
        ISplitProvider ForSplit();

        /// <summary>
        /// Provider de validação e armazenamento de cartões, que devolve um
        /// token reutilizável nas cobranças.
        /// </summary>
        ICardTokenProvider ForCardToken();

        /// <summary>
        /// Provider de criação da sessão de autenticação 3DS consumida pelo SDK
        /// de front-end.
        /// </summary>
        IAuthenticationSessionProvider ForAuthenticationSession();

        /// <summary>
        /// Provider de gerenciamento dos planos de assinatura.
        /// </summary>
        IPlanProvider ForPlan();

        /// <summary>
        /// Provider de gerenciamento dos assinantes das cobranças recorrentes.
        /// </summary>
        ICustomerProvider ForCustomer();

        /// <summary>
        /// Provider de gerenciamento das assinaturas.
        /// </summary>
        ISubscriptionProvider ForSubscription();

        /// <summary>
        /// Provider de gerenciamento dos cupons de desconto.
        /// </summary>
        ICouponProvider ForCoupon();

        /// <summary>
        /// Provider de consulta das faturas das assinaturas.
        /// </summary>
        IInvoiceProvider ForInvoice();

        /// <summary>
        /// Provider de consulta dos pagamentos das faturas e de criação de estornos.
        /// </summary>
        ISubscriptionPaymentProvider ForSubscriptionPayment();

        /// <summary>
        /// Provider das preferências de notificação e da chave pública das
        /// cobranças recorrentes.
        /// </summary>
        ISubscriptionPreferenceProvider ForSubscriptionPreference();
        /// <summary>
        /// Troca o código de autorização por um access_token e passa a
        /// utilizá-lo nas chamadas seguintes.
        /// <see href="https://developer.pagbank.com.br/reference/obter-access-token">ler documentação</see>
        /// </summary>
        /// <param name="authorizationCodeRequest">Código de autorização e dados do redirecionamento.</param>
        /// <returns>O access_token emitido.</returns>
        Task<AuthorizationCodeResponse> ConnectAsync(AuthorizationCodeRequest authorizationCodeRequest);
        /// <summary>
        /// Autentica pelo fluxo de desafio (challenge) e passa a utilizar
        /// o access_token e o desafio decriptado nas chamadas seguintes.
        /// <see href="https://developer.pagbank.com.br/reference/solicitar-autorizacao-via-connect-authorization">ler documentação</see>
        /// </summary>
        Task ConnectChallengeAsync();

        /// <summary>
        /// Renova o access_token a partir do refresh_token e passa a utilizá-lo
        /// nas chamadas seguintes.
        /// <see href="https://developer.pagbank.com.br/reference/renovar-access-token">ler documentação</see>
        /// </summary>
        /// <param name="refreshTokenRequest">Refresh token recebido na emissão anterior.</param>
        /// <returns>O novo access_token emitido, com um novo refresh_token.</returns>
        Task<AuthorizationCodeResponse> RefreshAccessTokenAsync(RefreshTokenRequest refreshTokenRequest);
        /// <summary>
        /// Configura o clientId e o clientSecret da aplicação
        /// utilizados nas chamadas que exigem identificação da aplicação.
        /// </summary>
        /// <param name="clientId">Identificador público da aplicação.</param>
        /// <param name="clientSecret">Chave secreta da aplicação.</param>
        void ConfigureClientApplication(string clientId, string clientSecret);
    }
}
