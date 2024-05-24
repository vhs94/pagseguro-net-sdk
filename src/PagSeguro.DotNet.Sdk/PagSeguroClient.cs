using AutoMapper;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PagSeguro.DotNet.Sdk.Account.Helpers;
using PagSeguro.DotNet.Sdk.Account.Interfaces;
using PagSeguro.DotNet.Sdk.Certificate.Helpers;
using PagSeguro.DotNet.Sdk.Certificate.Interfaces;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Interfaces;
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
        public PagSeguroSettings Settings { get; private set; }
        private ServiceCollection _services;
        private IServiceProvider _serviceProvider => _services.BuildServiceProvider();
        private IMapper _mapper => _serviceProvider.GetService<IMapper>();
        public virtual IAuthorizationProvider ForAuthorization()
            => _serviceProvider.GetService<IAuthorizationProvider>();
        public virtual IApplicationProvider ForApplication()
            => _serviceProvider.GetService<IApplicationProvider>();
        public virtual IAccountProvider ForAccount()
            => _serviceProvider.GetService<IAccountProvider>();
        public virtual IPublicKeyProvider ForPublicKey()
            => _serviceProvider.GetService<IPublicKeyProvider>();
        public virtual IOrderProvider ForOrder()
            => _serviceProvider.GetService<IOrderProvider>();
        public virtual IChargeWithPaymentMethodProvider ForCharge()
            => _serviceProvider.GetService<IChargeWithPaymentMethodProvider>();
        public virtual IDigitalCertificateProvider ForCertificate()
            => _serviceProvider.GetService<IDigitalCertificateProvider>();
        public virtual IFeeProvider ForFee()
            => _serviceProvider.GetService<IFeeProvider>();

        private IPagSeguroHttpExceptionFactory _pagSeguroHttpExceptionFactory
            => _serviceProvider.GetService<IPagSeguroHttpExceptionFactory>();

        public PagSeguroClient(ClientSettings settings)
        {
            CreateServiceCollection();
            ConfigureFlurlHttp();
            MapSettings(settings);
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
            FlurlHttp.Configure(settings =>
            {
                var jsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),

                };
                var defaultSerializer = new NewtonsoftJsonSerializer(jsonSettings);
                settings.JsonSerializer = defaultSerializer;
                settings.OnErrorAsync = HandleExceptionAsync;
            });
        }

        private async Task HandleExceptionAsync(FlurlCall call)
        {
            throw await _pagSeguroHttpExceptionFactory.CreateHttpExceptionAsync(call.Response);
        }

        private void MapSettings(ClientSettings settings)
        {
            Settings = _mapper.Map<PagSeguroSettings>(settings);
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
