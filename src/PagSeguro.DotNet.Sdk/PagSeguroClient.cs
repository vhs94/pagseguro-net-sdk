using AutoMapper;
using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.Account.Helpers;
using PagSeguro.DotNet.Sdk.Account.Interfaces;
using PagSeguro.DotNet.Sdk.Certificate.Helpers;
using PagSeguro.DotNet.Sdk.Checkout.Helpers;
using PagSeguro.DotNet.Sdk.Subscriptions.Helpers;
using PagSeguro.DotNet.Sdk.Subscriptions.Interfaces;
using PagSeguro.DotNet.Sdk.Checkout.Interfaces;
using PagSeguro.DotNet.Sdk.Certificate.Interfaces;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Common.Serialization;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Connect.Helpers;
using PagSeguro.DotNet.Sdk.Connect.Interfaces;
using PagSeguro.DotNet.Sdk.Connect.Models.Requests;
using PagSeguro.DotNet.Sdk.Connect.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Fees;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;
using PagSeguro.DotNet.Sdk.PublicKey.Helpers;
using PagSeguro.DotNet.Sdk.PublicKey.Interfaces;
using PagSeguro.DotNet.Sdk.Settings;

namespace PagSeguro.DotNet.Sdk
{
    public class PagSeguroClient : IPagSeguroClient
    {
        public PagSeguroSettings Settings { get; private set; } = null!;
        private readonly string _flurlClientName = $"PagSeguroClient-{Guid.NewGuid()}";
        private IServiceCollection _services = null!;
        private IServiceProvider _serviceProvider = null!;
        private IServiceProvider ServiceProvider => _serviceProvider;
        private bool _disposed;
        public virtual IAuthorizationProvider ForAuthorization()
            => ServiceProvider.GetRequiredService<IAuthorizationProvider>();
        public virtual IApplicationProvider ForApplication()
            => ServiceProvider.GetRequiredService<IApplicationProvider>();
        public virtual IAccountProvider ForAccount()
            => ServiceProvider.GetRequiredService<IAccountProvider>();
        public virtual IPublicKeyProvider ForPublicKey()
            => ServiceProvider.GetRequiredService<IPublicKeyProvider>();
        public virtual IOrderProvider ForOrder()
            => ServiceProvider.GetRequiredService<IOrderProvider>();
        public virtual IChargeWithPaymentMethodProvider ForCharge()
            => ServiceProvider.GetRequiredService<IChargeWithPaymentMethodProvider>();
        public virtual IDigitalCertificateProvider ForCertificate()
            => ServiceProvider.GetRequiredService<IDigitalCertificateProvider>();
        public virtual IFeeProvider ForFee()
            => ServiceProvider.GetRequiredService<IFeeProvider>();
        public virtual ICheckoutProvider ForCheckout()
            => ServiceProvider.GetRequiredService<ICheckoutProvider>();
        public virtual IPlanProvider ForPlan()
            => ServiceProvider.GetRequiredService<IPlanProvider>();
        public virtual ICustomerProvider ForCustomer()
            => ServiceProvider.GetRequiredService<ICustomerProvider>();
        public virtual ISubscriptionProvider ForSubscription()
            => ServiceProvider.GetRequiredService<ISubscriptionProvider>();
        public virtual ICouponProvider ForCoupon()
            => ServiceProvider.GetRequiredService<ICouponProvider>();
        public virtual IInvoiceProvider ForInvoice()
            => ServiceProvider.GetRequiredService<IInvoiceProvider>();
        public virtual ISubscriptionPaymentProvider ForSubscriptionPayment()
            => ServiceProvider.GetRequiredService<ISubscriptionPaymentProvider>();
        public virtual ISubscriptionPreferenceProvider ForSubscriptionPreference()
            => ServiceProvider.GetRequiredService<ISubscriptionPreferenceProvider>();

        public PagSeguroClient(ClientSettings settings)
        {
            CreateServiceCollection();
            MapSettings(settings);
            _serviceProvider = _services.BuildServiceProvider();
        }

        private void CreateServiceCollection()
        {
            _services = new ServiceCollection();
            _services.AddSingleton(CreateFlurlClient());
            _services.AddPagSeguroCommon();
            _services.AddConnectClient();
            _services.AddCertificateClient();
            _services.AddAccountClient();
            _services.AddAPublicKeyClient();
            _services.AddOrderClient();
            _services.AddCheckoutClient();
            _services.AddSubscriptionsClient();
            _services.AddAutoMapper(typeof(PagSeguroClient));
        }

        private IFlurlClient CreateFlurlClient()
        {
            return FlurlHttp.Clients.GetOrAdd(_flurlClientName, configure: builder =>
            {
                builder.Settings.JsonSerializer = DefaultSerializer.Build();
                builder.OnError(HandleExceptionAsync);
            });
        }

        private async Task HandleExceptionAsync(FlurlCall call)
        {
            if (!call.Succeeded)
            {
                IPagSeguroHttpExceptionFactory exceptionFactory = ServiceProvider
                    .GetRequiredService<IPagSeguroHttpExceptionFactory>();
                throw await exceptionFactory.CreateHttpExceptionAsync(call.Response);
            }
        }

        private void MapSettings(ClientSettings settings)
        {
            using var mappingProvider = _services.BuildServiceProvider();
            IMapper mapper = mappingProvider.GetRequiredService<IMapper>();
            Settings = mapper.Map<PagSeguroSettings>(settings);
            _services.AddSingleton(Settings);
        }

        public async Task<AuthorizationCodeResponse> ConnectAsync(
            AuthorizationCodeRequest authorizationCodeRequest)
        {
            AuthorizationCodeResponse result = await ForAuthorization()
                .CreateAccessTokenByCodeAsync(authorizationCodeRequest);
            Settings.AccessToken = result.AccessToken;
            return result;
        }

        public async Task ConnectChallengeAsync()
        {
            ChallengeResponse result = await ForAuthorization()
                .CreateAccessTokenByChallengeAsync();
            Settings.AccessToken = result.AccessToken;
            Settings.Challenge = result.DecryptedChallenge;
        }

        public async Task<AuthorizationCodeResponse> RefreshAccessTokenAsync(
            RefreshTokenRequest refreshTokenRequest)
        {
            AuthorizationCodeResponse result = await ForAuthorization()
                .RefreshAccessTokenAsync(refreshTokenRequest);
            Settings.AccessToken = result.AccessToken;
            return result;
        }

        public void ConfigureClientApplication(
            string clientId,
            string clientSecret)
        {
            Settings.ClientId = clientId;
            Settings.ClientSecret = clientSecret;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                (_serviceProvider as IDisposable)?.Dispose();
                FlurlHttp.Clients.Remove(_flurlClientName);
            }

            _disposed = true;
        }

        public async ValueTask DisposeAsync()
        {
            if (_disposed)
            {
                return;
            }

            if (_serviceProvider is IAsyncDisposable asyncDisposableProvider)
            {
                await asyncDisposableProvider.DisposeAsync();
            }
            else
            {
                (_serviceProvider as IDisposable)?.Dispose();
            }

            FlurlHttp.Clients.Remove(_flurlClientName);
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}

