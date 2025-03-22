using AutoMapper;
using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PagSeguro.DotNet.Sdk.Account.Helpers;
using PagSeguro.DotNet.Sdk.Account.Interfaces;
using PagSeguro.DotNet.Sdk.Certificate.Helpers;
using PagSeguro.DotNet.Sdk.Certificate.Interfaces;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Common.Serialization;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.AuthorizationCode;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.Challenge;
using PagSeguro.DotNet.Sdk.Connect.Helpers;
using PagSeguro.DotNet.Sdk.Connect.Interfaces;
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
        private IServiceCollection _services = null!;
        private IServiceProvider ServiceProvider => _services.BuildServiceProvider();
        private IMapper Mapper => ServiceProvider.GetService<IMapper>()!;
        public virtual IAuthorizationProvider ForAuthorization()
            => ServiceProvider.GetService<IAuthorizationProvider>()!;
        public virtual IApplicationProvider ForApplication()
            => ServiceProvider.GetService<IApplicationProvider>()!;
        public virtual IAccountProvider ForAccount()
            => ServiceProvider.GetService<IAccountProvider>()!;
        public virtual IPublicKeyProvider ForPublicKey()
            => ServiceProvider.GetService<IPublicKeyProvider>()!;
        public virtual IOrderProvider ForOrder()
            => ServiceProvider.GetService<IOrderProvider>()!;
        public virtual IChargeWithPaymentMethodProvider ForCharge()
            => ServiceProvider.GetService<IChargeWithPaymentMethodProvider>()!;
        public virtual IDigitalCertificateProvider ForCertificate()
            => ServiceProvider.GetService<IDigitalCertificateProvider>()!;
        public virtual IFeeProvider ForFee()
            => ServiceProvider.GetService<IFeeProvider>()!;

        private IPagSeguroHttpExceptionFactory PagSeguroHttpExceptionFactory
            => ServiceProvider.GetService<IPagSeguroHttpExceptionFactory>()!;

        public PagSeguroClient(ClientSettings settings)
        {
            CreateServiceCollection();
            MapSettings(settings);
            ConfigureFlurlHttp();
            ConfigureSettings();
        }

        private void CreateServiceCollection()
        {
            _services = new ServiceCollection();
            _services.AddPagSeguroCommon();
            _services.AddConnectClient();
            _services.AddCertificateClient();
            _services.AddAccountClient();
            _services.AddAPublicKeyClient();
            _services.AddOrderClient();
            _services.AddAutoMapper(typeof(PagSeguroClient));
        }

        private void ConfigureFlurlHttp()
        {
            FlurlHttp
                .Clients.WithDefaults(config =>
                {
                    config.Settings.JsonSerializer = DefaultSerializer.Build();
                    config.OnError(HandleExceptionAsync);
                });
        }

        private async Task HandleExceptionAsync(FlurlCall call)
        {
            if (!call.Succeeded)
            {
                throw await PagSeguroHttpExceptionFactory.CreateHttpExceptionAsync(call.Response);
            }
        }

        private void MapSettings(ClientSettings settings)
        {
            Settings = Mapper.Map<PagSeguroSettings>(settings);
        }

        private void ConfigureSettings()
        {
            _services.RemoveAll<PagSeguroSettings>();
            _services.AddSingleton(Settings);
        }

        public async Task<AuthorizationCodeReadDto> ConnectAsync(
            AuthorizationCodeWriteDto authorizationCodeWriteDto)
        {
            AuthorizationCodeReadDto result = await ForAuthorization()
                .CreateAccessTokenByCodeAsync(authorizationCodeWriteDto);
            Settings.AccessToken = result.AccessToken;
            ConfigureSettings();
            return result;
        }

        public async Task ConnectChallengeAsync()
        {
            ChallengeReadDto result = await ForAuthorization()
                .CreateAccessTokenByChallengeAsync();
            Settings.AccessToken = result.AccessToken;
            Settings.Challenge = result.DecryptedChallenge;
            ConfigureSettings();
        }

        public void ConfigureClientApplication(
            string clientId,
            string clientSecret)
        {
            Settings.ClientId = clientId;
            Settings.ClientSecret = clientSecret;
            ConfigureSettings();
        }
    }
}
