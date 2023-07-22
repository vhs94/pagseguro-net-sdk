using AutoMapper;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PagSeguro.DotNet.Sdk.Certificate.Helpers;
using PagSeguro.DotNet.Sdk.Certificate.Interfaces;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.AuthorizationCode;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.Challenge;
using PagSeguro.DotNet.Sdk.Connect.Helpers;
using PagSeguro.DotNet.Sdk.Connect.Interfaces;
using PagSeguro.DotNet.Sdk.Settings;

namespace PagSeguro.DotNet.Sdk
{
    public class PagSeguroClient
    {
        private ServiceCollection _services;
        private IServiceProvider _serviceProvider => _services.BuildServiceProvider();
        private IMapper _mapper => _serviceProvider.GetService<IMapper>()!;
        public virtual IApplicationProvider Application => _serviceProvider.GetService<IApplicationProvider>()!;
        public virtual IAuthorizationProvider Authorization => _serviceProvider.GetService<IAuthorizationProvider>()!;
        public virtual IDigitalCertificateProvider DigitalCertificate
            => _serviceProvider.GetService<IDigitalCertificateProvider>()!;
        public PagSeguroSettings Settings { get; private set; }

        public PagSeguroClient(ClientSettings settings)
        {
            CreateServiceCollection();
            ConfigureFlurlHttp();
            MapSettings(settings);
            SetupSettings();
        }

        private void CreateServiceCollection()
        {
            _services = new ServiceCollection();
            _services.AddConnectClient();
            _services.AddCertificateClient();
            _services.AddAutoMapper(typeof(PagSeguroClient));
        }

        private void ConfigureFlurlHttp()
        {
            FlurlHttp.Configure(settings =>
            {
                var jsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                settings.JsonSerializer = new NewtonsoftJsonSerializer(jsonSettings);
            });
        }

        private void MapSettings(ClientSettings settings)
        {
            Settings = _mapper.Map<PagSeguroSettings>(settings);
        }

        private void SetupSettings()
        {
            _services.RemoveAll<PagSeguroSettings>();
            _services.AddSingleton(Settings);
        }

        public async Task ConnectAsync(AuthorizationCodeWriteDto authorizationCodeWriteDto)
        {
            AuthorizationCodeReadDto result = await Authorization.CreateAccessTokenByCodeAsync(
                authorizationCodeWriteDto);
            Settings.AccessToken = result.AccessToken;
            SetupSettings();
        }

        public async Task ConnectChallengeAsync(ChallengeWriteDto challengeWriteDto)
        {
            ChallengeReadDto result = await Authorization.CreateAccessTokenByChallengeAsync(
                challengeWriteDto);
            Settings.AccessToken = result.AccessToken;
            Settings.Challenge = result.DecryptedChallenge;
            SetupSettings();
        }
    }
}
