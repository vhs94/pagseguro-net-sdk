using Flurl;
using PagSeguro.DotNet.Sdk.Common.Settings;

namespace PagSeguro.DotNet.Sdk.Common.Interfaces
{
    public interface IProvider
    {
        public PagSeguroSettings Settings { get; set; }
        public Url BaseUrl { get; }
    }
}
