using Flurl.Http.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Common.Serialization
{
    public static class DefaultSerializer
    {
        public static DefaultJsonSerializer Build() => new(options: JsonOptions.Default);
    }

    public static class JsonOptions
    {
        public static readonly JsonSerializerOptions Default = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            WriteIndented = false
        };
    }
}
