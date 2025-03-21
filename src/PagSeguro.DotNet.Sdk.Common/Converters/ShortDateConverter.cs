using Newtonsoft.Json.Converters;

namespace PagSeguro.DotNet.Sdk.Common.Converters
{
    public class ShortDateConverter : IsoDateTimeConverter
    {
        public ShortDateConverter() => DateTimeFormat = "yyyy-MM-dd";
    }
}
