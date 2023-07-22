using AutoMapper;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Settings;

namespace PagSeguro.DotNet.Sdk.Maps
{
    public class ClientSettingsMap : Profile
    {
        public ClientSettingsMap()
        {
            CreateMap<ClientSettings, PagSeguroSettings>();
        }
    }
}
